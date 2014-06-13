using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace App3.ViewModels
{
    class BankingsQueueViewModel : MyCollectionView
    {

        private const int maxSwaps = 3;

        private static BankingsQueueViewModel moder = null;

        public static  BankingsQueueViewModel GetInstance()
        {
            if (moder == null )
            {
                moder = new BankingsQueueViewModel();
            }
            return moder;
        }

        private BankingsQueueViewModel()
        {
            Queues = new ObservableCollection<QueueViewModel>();
            modelIDs = new Dictionary<string, QueueViewModel>();
            swapsLeft = new Dictionary<string, int>();
            LoadData();
        }

        private void LoadData()
        {
            AddQueueViewModel(new QueueViewModel("1", 23.3, "Republic Bank"));
            AddQueueViewModel(new QueueViewModel("23", 45.6, "RBC"));
        }


        public ObservableCollection<QueueViewModel> Queues
        {
            get;

            private set;
        }

        private Dictionary<string, int> swapsLeft;
        private Dictionary<string, QueueViewModel> modelIDs;

        public void AddQueueViewModel(QueueViewModel model)
        {
            Queues.Add(model);
            modelIDs.Add(model.ID, model);
            swapsLeft.Add(model.ID, maxSwaps);
            this.OnPropertyChanged("Queues");
        }

        public int GetSwapsLeft(QueueViewModel model)
        {
            string id = model.ID;
            int swaps = 0;
            swapsLeft.TryGetValue(id, out swaps);
            return swaps;
        }

        public int GetSwapsLeft(string id)
        {
            QueueViewModel model = null;
            modelIDs.TryGetValue(id, out model);
            return (model == null) ? 0 : GetSwapsLeft(model);
            
        }

        public void swap(string id)
        {
            int swaps = GetSwapsLeft(id);
            if (swaps > 0)
            {
                swapsLeft.Remove(id);
                swapsLeft.Add(id, swaps - 1);
            }

            QueueViewModel qm = null;
            modelIDs.TryGetValue(id, out qm);
            qm.ResetColour(swaps - 1);
        }

        public void swap(QueueViewModel model)
        {
            swap(model.ID);
        }

        public void RemoveQueueViewModel(QueueViewModel model)
        {
            Queues.Remove(model);
            modelIDs.Remove(model.ID);
            swapsLeft.Remove(model.ID);
            this.OnPropertyChanged("Queues");
        }

        public void RemoveQueueViewModel(string ID)
        {
            QueueViewModel model = null;
            modelIDs.TryGetValue(ID, out model);
            Queues.Remove(model);
            modelIDs.Remove(ID);
            swapsLeft.Remove(ID);
        }

        public void QueryAllTimes()
        {
            foreach(QueueViewModel Q in Queues)
            {
                Q.QueryTimeLeft();
            }
        }
    }
}
