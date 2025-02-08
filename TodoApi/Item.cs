using System;
using System.Collections.Generic;

namespace TodoApi;

public partial class Item
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public bool? IsComplete { get; set; }
}
  // "my_db": "server=localhost;user=root;password=rha1828!;database=my_db"
//  "Server=busaqvzpmohlewvrqmq8-mysql.services.clever-cloud.com;Database=busaqvzpmohlewvrqmq8;User=ucb2fvhjxlectphj;Password=awvgIKkEx4RVJQU0ZKAP"
//  "ToDoListDB": "Server=busaqvzpmohlewvrqmq8-mysql.services.clever-cloud.com;Database=busaqvzpmohlewvrqmq8;User=ucb2fvhjxlectphj;Password=awvgIKkEx4RVJQU0ZKAP;"
