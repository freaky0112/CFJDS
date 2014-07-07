using System;
using System.Collections.Generic;
using System.Text;

namespace CFJDS {
    class DataCFSJ {
        /// <summary>
        /// 姓名
        /// </summary>
        private string _name;

        public string Name {
            get { return _name; }
            set { _name = value; }
        }
        /// <summary>
        /// 日期
        /// </summary>
        private int _date;

        public int Date {
            get { return _date; }
            set { _date = value; }
        }
        /// <summary>
        /// 地点
        /// </summary>
        private string _location;

        public string Location {
            get { return _location; }
            set { _location = value; }
        }
        /// <summary>
        /// 违法面积
        /// </summary>
        private double _area;

        public double Area {
            get { return _area; }
            set { _area = value; }
        }
        /// <summary>
        /// 单价
        /// </summary>
        private int _unit;

        public int Unit {
            get { return _unit; }
            set { _unit = value; }
        }

        /// <summary>
        /// 行为处罚金额
        /// </summary>
        private double _price;

        public double Price {
            get { return _price; }
            set { _price = value; }
        }
        /// <summary>
        /// 是否没收
        /// </summary>
        private bool _isnotConfiscate;

        public bool IsnotConfiscate {
            get { return _isnotConfiscate; }
            set { _isnotConfiscate = value; }
        }
        /// <summary>
        /// 没收面积
        /// </summary>
        private double _confiscateArea;

        public double ConfiscateArea {
            get { return _confiscateArea; }
            set { _confiscateArea = value; }
        }
        /// <summary>
        /// 没收单价
        /// </summary>
        private int _confiscateAreaUnit;

        public int ConfiscateAreaUnit {
            get { return _confiscateAreaUnit; }
            set { _confiscateAreaUnit = value; }
        }
        //编号
        private int _no;

        public int No {
            get { return _no; }
            set { _no = value; }
        }
        /// <summary>
        /// 没收价格
        /// </summary>
        private double _confiscateAreaPrice;

        public double ConfiscateAreaPrice {
            get { return _confiscateAreaPrice; }
            set { _confiscateAreaPrice = value; }
        }
        /// <summary>
        /// 户主名
        /// </summary>
        private string[] _names;

        public string[] Names {
            get {
                _names = Name.Split('、');
                return _names;
            }
            set { _names = value; }
        }

        /// <summary>
        /// 身份证
        /// </summary>
        private string _ID;

        public string ID {
            get { return _ID; }
            set { _ID = value; }
        }

        /// <summary>
        /// 身份证号
        /// </summary>
        private string[] _IDs;

        public string[] IDs {
            get { return ID.Split('、'); }
            set { _IDs = value; }
        }
        




        /// <summary>
        /// 出生日期
        /// </summary>
        DateTime[] _birthDate;

        public DateTime[] BirthDate {
            get {
                _birthDate=new DateTime[_IDs.Length];
                if (_IDs != null) {
                    for(int i=0;i<_IDs.Length;i++){
                        string id=_IDs[i];
                        if (!string.IsNullOrEmpty(id)) {
                            DateTime birthDate = DateTime.Parse(id.Substring(6, 8).Insert(6, "-").Insert(4, "-"));
                            _birthDate.SetValue(birthDate,i);
                        }
                    }
                }
                
                return _birthDate; 
                }
            set { _birthDate = value; }
        }


        /// <summary>
        /// 性别
        /// </summary>
        private string[] _sex;

        public string[] Sex {
            get {
                _sex = new string[_IDs.Length];
                if (_IDs!=null) {
                    for (int i = 0; i < _IDs.Length; i++) {                        
                        string id = _IDs[i];
                        if (!string.IsNullOrEmpty(id)) {
                            string sex = "";
                            if (Int32.Parse(id.Substring(16, 1)) % 2 == 0) {
                                sex = "女";
                            } else {
                                sex = "男";
                            }
                            _sex[i] = sex;
                        }
                    }
                }
                
                
                
                return _sex; }
            set {
                
                    _sex = value;
               
            }
        }

        private string _town;

        public string Town {
            get { return _town; }
            set { _town = value; }
        }


        private string _guid;

        public string Guid {
            get { return _guid; }
            set { _guid = value; }
        }

        private string _code;

        public string Code {
            get { return _code; }
            set { _code = value; }
        }

        private int _confiscateNo;

        public int ConfiscateNo {
            get { return _confiscateNo; }
            set { _confiscateNo = value; }
        }
    }
}
