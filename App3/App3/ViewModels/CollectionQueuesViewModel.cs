using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Collections.Specialized;

namespace App3.ViewModels
{
    class CollectionQueuesViewModel : MyCollectionView
    {
        public ObservableCollection<QueueViewModel> Queues
        {
            get;

            private set;
        }

        private Dictionary<string, QueueViewModel> modelIDs;
        private static CollectionQueuesViewModel model;

        public static CollectionQueuesViewModel GetInstance()
        {
            if (model == null)
                model = new CollectionQueuesViewModel();
            return model;
        }

        public void QueryAllTimes()
        {
            foreach (QueueViewModel Q in Queues)
            {
                Q.QueryTimeLeft();
            }
        }

        private CollectionQueuesViewModel()
        {
            Queues = new ObservableCollection<QueueViewModel>();
            modelIDs = new Dictionary<string, QueueViewModel>();
            LoadData();
        }

        private void LoadData()
        {
            AddQueueViewModel(new QueueViewModel("1", 23.3, "BK"));
            AddQueueViewModel(new QueueViewModel("23", 45.6, "Rituals"));
        }

        public void AddQueueViewModel(QueueViewModel model)
        {
            Queues.Add(model);
            modelIDs.Add(model.ID, model);
            this.OnPropertyChanged("Queues");
        }

        public void RemoveQueueViewModel(QueueViewModel model)
        {
            Queues.Remove(model);
            modelIDs.Remove(model.ID);
            this.OnPropertyChanged("Queues");
        }

        public void RemoveQueueViewModel(string id)
        {
            QueueViewModel model = null;
            modelIDs.TryGetValue(id, out model);
            if (model != null)
            {
                RemoveQueueViewModel(model);
            }
        }
    }
}
