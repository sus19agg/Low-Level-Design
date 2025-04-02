using FileSystem.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem.Models
{
    internal class Metadata
    {
        public required string createdOn { get; set; }
        public required string owner { get; set; }
        public required string lastModifiedOn { get; set; }
        public FileEntityType fileEntityType { get; set; }
    }
}
