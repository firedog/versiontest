using System;
using NPoco;

namespace FailingVersionColumn.Models
{
    public class VersionTest
    {
        public Guid Id { get; set; }

        // Works
        //        [VersionColumn("Version", VersionColumnType.Number)]


        // Fails
        [VersionColumn(VersionColumnType.Number)]
        public long Version { get; set; }
    }
}
