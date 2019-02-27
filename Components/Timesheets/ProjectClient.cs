using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace Timesheets
{
    public class ProjectClient : IProjectClient
    {
        private readonly HttpClient _client;
        private readonly ILogger<ProjectClient> logger;
        private readonly IDictionary<long, ProjectInfo> projectCache = new Dictionary<long, ProjectInfo>();

        public ProjectClient(HttpClient client, ILogger<ProjectClient> logger)
        {
            _client = client;
            this.logger = logger;
        }

        private Task<ProjectInfo> DoGetFromCache(long projectId)
        {
            return Task.FromResult(projectCache[projectId]);
        }

        private async Task<ProjectInfo> DoGet(long projectId)
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            var streamTask = _client.GetStreamAsync($"project?projectId={projectId}");

            logger.LogInformation($"Attempting to fetch projectId: {projectId}");

            var serializer = new DataContractJsonSerializer(typeof(ProjectInfo));
            var projectInfo = serializer.ReadObject(await streamTask) as ProjectInfo;

            projectCache[projectId] = projectInfo;
            logger.LogInformation($"Caching projectId: {projectId}");
            return projectInfo;
        }

        public async Task<ProjectInfo> Get(long projectId) =>
            await new GetProjectCommand(DoGet, DoGetFromCache, projectId).ExecuteAsync();
    }
}