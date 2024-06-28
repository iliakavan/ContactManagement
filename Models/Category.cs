using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManagementV2.Models;

public class Category
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public ICollection<Contact>? Contacts { get; set; }
}
