using System;
using System.Collections.Generic;

namespace Atlas.UI.Models
{
    public class PagedResultViewModel<TResult>
    {
        public int Page { get; set; }
        public int Size { get; set; }
        public IEnumerable<TResult> Result { get; set; }
        public int Count { get; set; }

        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)Count / Size); }
        }
    }
}