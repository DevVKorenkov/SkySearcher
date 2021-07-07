using SkySearcher.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkySearcher.ViewModel
{
    class ErrorsViewModel
    {
        public List<PcSaveErrors> Errors { get; set; }

        public ErrorsViewModel(List<PcSaveErrors> errors)
        {
            Errors = errors;
        }

        public ErrorsViewModel()
        { }
    }
}
