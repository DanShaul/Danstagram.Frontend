using Danstagram.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Danstagram.ViewModels
{
    class ViewModelBase<TModel> : BindableBase where TModel: class,new()
    {
        bool isBusy = false;
        public bool IsBusy{get { return isBusy; } set { SetProperty(ref isBusy, value); } }

        private TModel model = new TModel();
        public TModel Model { get { return model; } set { SetProperty(ref model, value); } }

        private string title = "";
        public string Title { get { return title; } set { SetProperty(ref title, value); } }

    }
}
