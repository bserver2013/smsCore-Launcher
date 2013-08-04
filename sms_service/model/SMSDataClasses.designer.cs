﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18047
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace sms_service.model
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="SMSDatabase")]
	public partial class SMSDataClassesDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertSMS_Inbox(SMS_Inbox instance);
    partial void UpdateSMS_Inbox(SMS_Inbox instance);
    partial void DeleteSMS_Inbox(SMS_Inbox instance);
    partial void InsertSMS_QueuedBox(SMS_QueuedBox instance);
    partial void UpdateSMS_QueuedBox(SMS_QueuedBox instance);
    partial void DeleteSMS_QueuedBox(SMS_QueuedBox instance);
    #endregion
		
		public SMSDataClassesDataContext() : 
				base(global::sms_service.Properties.Settings.Default.SMSDatabaseConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public SMSDataClassesDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public SMSDataClassesDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public SMSDataClassesDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public SMSDataClassesDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<SMS_Inbox> SMS_Inboxes
		{
			get
			{
				return this.GetTable<SMS_Inbox>();
			}
		}
		
		public System.Data.Linq.Table<SMS_QueuedBox> SMS_QueuedBoxes
		{
			get
			{
				return this.GetTable<SMS_QueuedBox>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.SMS_Inbox")]
	public partial class SMS_Inbox : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private long _Id;
		
		private string _Sender;
		
		private string _Message;
		
		private System.Nullable<System.DateTime> _DateReceived;
		
		private System.Nullable<int> _MonthOf;
		
		private System.Nullable<long> _YearOf;
		
		private System.Nullable<bool> _Status;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(long value);
    partial void OnIdChanged();
    partial void OnSenderChanging(string value);
    partial void OnSenderChanged();
    partial void OnMessageChanging(string value);
    partial void OnMessageChanged();
    partial void OnDateReceivedChanging(System.Nullable<System.DateTime> value);
    partial void OnDateReceivedChanged();
    partial void OnMonthOfChanging(System.Nullable<int> value);
    partial void OnMonthOfChanged();
    partial void OnYearOfChanging(System.Nullable<long> value);
    partial void OnYearOfChanged();
    partial void OnStatusChanging(System.Nullable<bool> value);
    partial void OnStatusChanged();
    #endregion
		
		public SMS_Inbox()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="BigInt NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public long Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Sender", DbType="VarChar(13)")]
		public string Sender
		{
			get
			{
				return this._Sender;
			}
			set
			{
				if ((this._Sender != value))
				{
					this.OnSenderChanging(value);
					this.SendPropertyChanging();
					this._Sender = value;
					this.SendPropertyChanged("Sender");
					this.OnSenderChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Message", DbType="VarChar(200)")]
		public string Message
		{
			get
			{
				return this._Message;
			}
			set
			{
				if ((this._Message != value))
				{
					this.OnMessageChanging(value);
					this.SendPropertyChanging();
					this._Message = value;
					this.SendPropertyChanged("Message");
					this.OnMessageChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DateReceived", DbType="DateTime")]
		public System.Nullable<System.DateTime> DateReceived
		{
			get
			{
				return this._DateReceived;
			}
			set
			{
				if ((this._DateReceived != value))
				{
					this.OnDateReceivedChanging(value);
					this.SendPropertyChanging();
					this._DateReceived = value;
					this.SendPropertyChanged("DateReceived");
					this.OnDateReceivedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MonthOf", DbType="Int")]
		public System.Nullable<int> MonthOf
		{
			get
			{
				return this._MonthOf;
			}
			set
			{
				if ((this._MonthOf != value))
				{
					this.OnMonthOfChanging(value);
					this.SendPropertyChanging();
					this._MonthOf = value;
					this.SendPropertyChanged("MonthOf");
					this.OnMonthOfChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_YearOf", DbType="BigInt")]
		public System.Nullable<long> YearOf
		{
			get
			{
				return this._YearOf;
			}
			set
			{
				if ((this._YearOf != value))
				{
					this.OnYearOfChanging(value);
					this.SendPropertyChanging();
					this._YearOf = value;
					this.SendPropertyChanged("YearOf");
					this.OnYearOfChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Status", DbType="Bit")]
		public System.Nullable<bool> Status
		{
			get
			{
				return this._Status;
			}
			set
			{
				if ((this._Status != value))
				{
					this.OnStatusChanging(value);
					this.SendPropertyChanging();
					this._Status = value;
					this.SendPropertyChanged("Status");
					this.OnStatusChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.SMS_QueuedBox")]
	public partial class SMS_QueuedBox : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ID;
		
		private string _Number;
		
		private string _Message;
		
		private System.Nullable<short> _Network;
		
		private System.Nullable<bool> _Status;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnNumberChanging(string value);
    partial void OnNumberChanged();
    partial void OnMessageChanging(string value);
    partial void OnMessageChanged();
    partial void OnNetworkChanging(System.Nullable<short> value);
    partial void OnNetworkChanged();
    partial void OnStatusChanging(System.Nullable<bool> value);
    partial void OnStatusChanged();
    #endregion
		
		public SMS_QueuedBox()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Number", DbType="VarChar(11)")]
		public string Number
		{
			get
			{
				return this._Number;
			}
			set
			{
				if ((this._Number != value))
				{
					this.OnNumberChanging(value);
					this.SendPropertyChanging();
					this._Number = value;
					this.SendPropertyChanged("Number");
					this.OnNumberChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Message", DbType="VarChar(MAX)")]
		public string Message
		{
			get
			{
				return this._Message;
			}
			set
			{
				if ((this._Message != value))
				{
					this.OnMessageChanging(value);
					this.SendPropertyChanging();
					this._Message = value;
					this.SendPropertyChanged("Message");
					this.OnMessageChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Network", DbType="SmallInt")]
		public System.Nullable<short> Network
		{
			get
			{
				return this._Network;
			}
			set
			{
				if ((this._Network != value))
				{
					this.OnNetworkChanging(value);
					this.SendPropertyChanging();
					this._Network = value;
					this.SendPropertyChanged("Network");
					this.OnNetworkChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Status", DbType="Bit")]
		public System.Nullable<bool> Status
		{
			get
			{
				return this._Status;
			}
			set
			{
				if ((this._Status != value))
				{
					this.OnStatusChanging(value);
					this.SendPropertyChanging();
					this._Status = value;
					this.SendPropertyChanged("Status");
					this.OnStatusChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
