using TestApplication.ViewModels.Common;
using System.Collections.Generic;

namespace TestApplication.Models
{
    public class ComboBoxTest_Model
    {
        public List<ComboBox_Dto> CreateItemsSource()
        {
            var src = new List<ComboBox_Dto>();
            for (var i = 0; i < 10; i++)
            {
                src.Add(new ComboBox_Dto()
                {
                    Name = "Name of " + i.ToString(),
                    Value = i.ToString()
                });
            }
            return src;
        }

    }
}
