using System.Collections.Generic;

namespace PhoneBookBackEnd.ViewModels
{
  public class DeleteContactsViewModel
  {
    public List<int> ContactIds { get; set; } = new List<int>();
  }
}