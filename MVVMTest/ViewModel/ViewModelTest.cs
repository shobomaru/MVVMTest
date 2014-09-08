using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.MVVM;
using System.ComponentModel;
using MVVMTest.Model;

namespace MVVMTest.ViewModel
{
    /// <summary>
    /// テストViewModel
    /// </summary>
    public class ViewModelTest : ViewModelBase
    {
        private ModelTest _modelTest;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// Model(XAML)から生成される。
        public ViewModelTest()
        {
            // モデルの作成
            _modelTest = new ModelTest();

            // Modelが変更されたときにViewModelが受け取るイベント
            // そのままViewModelからViewへ通知する。
            _modelTest.PropertyChanged += ModelPropertyChanged;

            // コマンドの作成
            // Viewから呼び出され、そのときの動作を定義する。
            SetTextCommand = new RelayCommand(() =>
            {
                Console.WriteLine("ViewModelTestのSetTextCommandが呼び出された。テキストを更新する。");
                TextProp = _textInput;
            });

            // 初期値
            TextProp = "テキストボックスに何か入力してください";
        }

        ~ViewModelTest()
        {
            // ModelからViewModelの参照を外す。
            // もしModelがこのViewModel以外から参照されていると、明示的にイベントから除去しなければメモリリークを起こす。
            _modelTest.PropertyChanged -= ModelPropertyChanged;
        }


        #region Modelとやりとりするための関数

        /// <summary>
        /// ModelでRaisePropertyChangedが呼び出されたとき、そのプロパティ名を受け取り、Viewへ伝えるためのコールバック関数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged(e.PropertyName);
        }

        #endregion


        #region Viewとやりとりするためのプロパティ

        /// <summary>
        /// テキストのProperty
        /// </summary>
        /// 本当はプロパティ名をTextにしたかったが、XAMLのText属性と混同する可能性があるため別名にしている。
        public string TextProp
        {
            get { return _modelTest.TextProp; }
            set
            {
                // ViewとViewModelで値が異なれば、ViewModelを更新する。
                if (value == _modelTest.TextProp)
                    return;
                Console.WriteLine("ViewModelTestのTextPropを更新している。値：" + value);

                // ViewModelに伝えられた変更内容を、Modelに反映する。
                _modelTest.TextProp = value;

                // 仮にModelがINotifyPropertyChangedを実装しておらず、ViewModelがModelにPropertyChangedを追加していなければ、下のコードが必要。
                // Viewに変更を通知する。
                //RaisePropertyChanged("TextProp");
            }
        }

        /// <summary>
        /// 入力中のテキスト
        /// </summary>
        private string _textInput;

        /// <summary>
        /// 入力中のテキストを受け取るProperty
        /// </summary>
        /// Binding ModeがOneWayToSourceであることを期待している。Viewには何も通知しない。
        public string TextInputProp
        {
            get { return _textInput; }
            set
            {
                Console.WriteLine("ViewModelTestのTextInputPropを更新している。値：" + value);
                _textInput = value;
            }
        }

        #endregion


        #region Viewから発行されるコマンド

        /// <summary>
        /// テキストを設定するコマンド
        /// </summary>
        public RelayCommand SetTextCommand
        {
            get;
            private set;
        }

        #endregion
    }
}
