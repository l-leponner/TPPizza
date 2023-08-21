using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace TPPizza.CustomAttributes
{
    public class TestAttribute : ValidationAttribute
    {
        public int Min { get; set; }
        public override bool IsValid(object value)
        {
            var list = value as IList;
            if (list != null)
            {
                if (list.Count < Min)
                    return false;
                return list.Count > 0;
            }


            return false;
        }
    }
}
