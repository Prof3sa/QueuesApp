using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App3.ViewModels
{
    class QueueViewModel : MyViewModel
    {

        private string _id;
        private int _estimatedTime;
        private string _queueOwner;
        

        public QueueViewModel(string id, double estimatedTime, string queueOwner)
        {
            ID = id;
            _estimatedTime = (int)Math.Ceiling(estimatedTime);
            //timer = new Timer(estimatedTime);
            //EstimatedTime = timer.GetMinutesLeft();
            _queueOwner = queueOwner;
            _color = "Black";
        }

        public string GetEstimation
        {
            get
            {
                string str = "";
                str = "Estimated Time : " + this.EstimatedTime.ToString() + " minutes";
                return str;
            }
        }

        public string GetButtonText
        {
            get
            {
                string repr = this.QueueOwner + "\n";
                repr = repr + GetEstimation;
                return repr;
            }
        }

        public string QueueOwner
        {
            get { return _queueOwner; }
            set { _queueOwner = value; this.OnPropertyChanged("QueueOwner"); }
        }

        public void swap()
        {
            this.OnPropertyChanged("EnableSwap");
            this.OnPropertyChanged("Color");
        }

        public bool EnableSwap
        {
            get
            {
                BankingsQueueViewModel bank = BankingsQueueViewModel.GetInstance();
                return bank.GetSwapsLeft(this) > 0;
            }
        }

        public void ResetColour(int num)
        {
            if (num == 3) Color =  "Green";
            else if (num == 2) Color =  "Yellow";
            else if (num == 1) Color =  "Red";
            else Color = "Gray";
        }

        private string _color;

        public string Color
        {
            get
            {
                return _color;
            }

            set
            {
                _color = value;
                this.OnPropertyChanged("Color");
            }

                

            
        }

        public override bool Equals(object obj)
        {
            if(obj == null)
            {
                return false;
            }

            QueueViewModel model = obj as QueueViewModel;
            if (model == null)
            {
                return false;
            }

            return this.ID.Equals(model.ID);
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

        public override string ToString()
        {
            string repr = "{ ";
            repr = ID + " , " + EstimatedTime + " }";
            return repr;
        }

        public string ID
        {
            get { return _id;  }
            set
            {
                _id = value;
                this.OnPropertyChanged("ID");
            }
        }

        public int EstimatedTime
        {
            get { return _estimatedTime; }
            set
            {
                _estimatedTime = value;
                this.OnPropertyChanged("EstimatedTime");
                this.OnPropertyChanged("GetEstimation");
                this.OnPropertyChanged("GetButtonText");
            }
        }

        public void QueryTimeLeft()
        {
            _estimatedTime -= 1;
            this.OnPropertyChanged("EstimatedTime");
            this.OnPropertyChanged("GetEstimation");
            this.OnPropertyChanged("GetButtonText");
           
        }
      

    }
}
