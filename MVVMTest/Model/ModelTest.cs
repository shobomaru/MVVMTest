using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.MVVM;
using System.ComponentModel;

namespace MVVMTest.Model
{
    /// <summary>
    /// Testモデル
    /// </summary>
    public class ModelTest : INotifyPropertyChanged
    {
        #region INotiftPropertyChangedの実装

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged(string name)
        {
            var ev = PropertyChanged;
            if (ev == null)
                return;
            ev(this, new PropertyChangedEventArgs(name));
        }

        #endregion


        #region プロパティ

        /// <summary>
        /// テキスト
        /// </summary>
        private string _text;

        /// <summary>
        /// テキストのプロパティ
        /// </summary>
        public string TextProp
        {
            get { return _text; }
            set
            {
                // ViewModelとModelで値が異なれば、Modelを更新する。
                if (value == _text)
                    return;
                Console.WriteLine("ModelTestのTextを更新している。値：" + value);
                _text = value;

                // ViewModelに変更を通知する。
                RaisePropertyChanged("TextProp");
            }
        }

        #endregion
    }
}
