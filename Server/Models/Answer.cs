using System;
using System.Collections.Generic;

namespace Server.Models;

public partial class Answer
{
    public string Id { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string? Questionid { get; set; }

    public virtual Question? Question { get; set; }
}
