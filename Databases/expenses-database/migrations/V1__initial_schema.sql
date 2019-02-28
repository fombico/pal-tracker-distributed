create table expenses (
  id bigint(20) not null auto_increment,
  user_id bigint(20),
  project_id bigint(20),
  expense_type varchar(20),
  total_amount_spent double,
  date datetime,

  primary key (id)
)
engine = innodb
default charset = utf8;
