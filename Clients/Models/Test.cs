using System;
using System.Collections.Generic;

namespace Clients.Models;

public partial class Test
{
    public string Id { get; set; } = null!;

    public string Code { get; set; } = null!;

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
