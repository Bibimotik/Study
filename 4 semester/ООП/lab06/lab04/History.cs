using System.Collections.Generic;
using System.Windows.Documents;

namespace lab04
{
    public class History
    {
        public List<Product> HistoryList = new List<Product>();
        
        public void AddProductToHistory(Product _product)
        {
            HistoryList.Add(_product);
        }
    }
}