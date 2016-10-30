﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.42000
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NoobDevBot
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
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="NoobBotDatabase")]
	public partial class NoobBotDatabaseDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Definitionen der Erweiterungsmethoden
    partial void OnCreated();
    partial void Insertuser(user instance);
    partial void Updateuser(user instance);
    partial void Deleteuser(user instance);
    partial void Insertstreams(streams instance);
    partial void Updatestreams(streams instance);
    partial void Deletestreams(streams instance);
    partial void Insertgroups(groups instance);
    partial void Updategroups(groups instance);
    partial void Deletegroups(groups instance);
    #endregion
		
		public NoobBotDatabaseDataContext() : 
				base(global::NoobDevBot.Properties.Settings.Default.NoobBotDatabaseConnectionString1, mappingSource)
		{
			OnCreated();
		}
		
		public NoobBotDatabaseDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public NoobBotDatabaseDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public NoobBotDatabaseDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public NoobBotDatabaseDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<user> user
		{
			get
			{
				return this.GetTable<user>();
			}
		}
		
		public System.Data.Linq.Table<streams> streams
		{
			get
			{
				return this.GetTable<streams>();
			}
		}
		
		public System.Data.Linq.Table<groups> groups
		{
			get
			{
				return this.GetTable<groups>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.[user]")]
	public partial class user : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _id;
		
		private int _group;
		
		private string _name;
		
		private EntitySet<streams> _streams;
		
		private EntityRef<groups> _groups;
		
    #region Definitionen der Erweiterungsmethoden
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnidChanging(int value);
    partial void OnidChanged();
    partial void OngroupChanging(int value);
    partial void OngroupChanged();
    partial void OnnameChanging(string value);
    partial void OnnameChanged();
    #endregion
		
		public user()
		{
			this._streams = new EntitySet<streams>(new Action<streams>(this.attach_streams), new Action<streams>(this.detach_streams));
			this._groups = default(EntityRef<groups>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this.OnidChanging(value);
					this.SendPropertyChanging();
					this._id = value;
					this.SendPropertyChanged("id");
					this.OnidChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="[group]", Storage="_group", DbType="Int NOT NULL")]
		public int group
		{
			get
			{
				return this._group;
			}
			set
			{
				if ((this._group != value))
				{
					this.OngroupChanging(value);
					this.SendPropertyChanging();
					this._group = value;
					this.SendPropertyChanged("group");
					this.OngroupChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_name", DbType="VarChar(MAX)")]
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				if ((this._name != value))
				{
					this.OnnameChanging(value);
					this.SendPropertyChanging();
					this._name = value;
					this.SendPropertyChanged("name");
					this.OnnameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="user_streams", Storage="_streams", ThisKey="id", OtherKey="userId")]
		public EntitySet<streams> streams
		{
			get
			{
				return this._streams;
			}
			set
			{
				this._streams.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="groups_user", Storage="_groups", ThisKey="group", OtherKey="id", IsForeignKey=true)]
		public groups groups
		{
			get
			{
				return this._groups.Entity;
			}
			set
			{
				groups previousValue = this._groups.Entity;
				if (((previousValue != value) 
							|| (this._groups.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._groups.Entity = null;
						previousValue.user.Remove(this);
					}
					this._groups.Entity = value;
					if ((value != null))
					{
						value.user.Add(this);
						this._group = value.id;
					}
					else
					{
						this._group = default(int);
					}
					this.SendPropertyChanged("groups");
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
		
		private void attach_streams(streams entity)
		{
			this.SendPropertyChanging();
			entity.user = this;
		}
		
		private void detach_streams(streams entity)
		{
			this.SendPropertyChanging();
			entity.user = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.streams")]
	public partial class streams : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _id;
		
		private int _userId;
		
		private System.DateTime _start;
		
		private string _title;
		
		private string _url;
		
		private EntityRef<user> _user;
		
    #region Definitionen der Erweiterungsmethoden
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnidChanging(int value);
    partial void OnidChanged();
    partial void OnuserIdChanging(int value);
    partial void OnuserIdChanged();
    partial void OnstartChanging(System.DateTime value);
    partial void OnstartChanged();
    partial void OntitleChanging(string value);
    partial void OntitleChanged();
    partial void OnurlChanging(string value);
    partial void OnurlChanged();
    #endregion
		
		public streams()
		{
			this._user = default(EntityRef<user>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this.OnidChanging(value);
					this.SendPropertyChanging();
					this._id = value;
					this.SendPropertyChanged("id");
					this.OnidChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_userId", DbType="Int NOT NULL")]
		public int userId
		{
			get
			{
				return this._userId;
			}
			set
			{
				if ((this._userId != value))
				{
					if (this._user.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnuserIdChanging(value);
					this.SendPropertyChanging();
					this._userId = value;
					this.SendPropertyChanged("userId");
					this.OnuserIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_start", DbType="DateTime NOT NULL")]
		public System.DateTime start
		{
			get
			{
				return this._start;
			}
			set
			{
				if ((this._start != value))
				{
					this.OnstartChanging(value);
					this.SendPropertyChanging();
					this._start = value;
					this.SendPropertyChanged("start");
					this.OnstartChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_title", DbType="VarChar(MAX) NOT NULL", CanBeNull=false)]
		public string title
		{
			get
			{
				return this._title;
			}
			set
			{
				if ((this._title != value))
				{
					this.OntitleChanging(value);
					this.SendPropertyChanging();
					this._title = value;
					this.SendPropertyChanged("title");
					this.OntitleChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_url", DbType="VarChar(MAX)")]
		public string url
		{
			get
			{
				return this._url;
			}
			set
			{
				if ((this._url != value))
				{
					this.OnurlChanging(value);
					this.SendPropertyChanging();
					this._url = value;
					this.SendPropertyChanged("url");
					this.OnurlChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="user_streams", Storage="_user", ThisKey="userId", OtherKey="id", IsForeignKey=true)]
		public user user
		{
			get
			{
				return this._user.Entity;
			}
			set
			{
				user previousValue = this._user.Entity;
				if (((previousValue != value) 
							|| (this._user.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._user.Entity = null;
						previousValue.streams.Remove(this);
					}
					this._user.Entity = value;
					if ((value != null))
					{
						value.streams.Add(this);
						this._userId = value.id;
					}
					else
					{
						this._userId = default(int);
					}
					this.SendPropertyChanged("user");
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.groups")]
	public partial class groups : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _id;
		
		private string _name;
		
		private int _power;
		
		private EntitySet<user> _user;
		
    #region Definitionen der Erweiterungsmethoden
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnidChanging(int value);
    partial void OnidChanged();
    partial void OnnameChanging(string value);
    partial void OnnameChanged();
    partial void OnpowerChanging(int value);
    partial void OnpowerChanged();
    #endregion
		
		public groups()
		{
			this._user = new EntitySet<user>(new Action<user>(this.attach_user), new Action<user>(this.detach_user));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this.OnidChanging(value);
					this.SendPropertyChanging();
					this._id = value;
					this.SendPropertyChanged("id");
					this.OnidChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_name", DbType="VarChar(MAX) NOT NULL", CanBeNull=false)]
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				if ((this._name != value))
				{
					this.OnnameChanging(value);
					this.SendPropertyChanging();
					this._name = value;
					this.SendPropertyChanged("name");
					this.OnnameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_power", DbType="Int NOT NULL")]
		public int power
		{
			get
			{
				return this._power;
			}
			set
			{
				if ((this._power != value))
				{
					this.OnpowerChanging(value);
					this.SendPropertyChanging();
					this._power = value;
					this.SendPropertyChanged("power");
					this.OnpowerChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="groups_user", Storage="_user", ThisKey="id", OtherKey="group")]
		public EntitySet<user> user
		{
			get
			{
				return this._user;
			}
			set
			{
				this._user.Assign(value);
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
		
		private void attach_user(user entity)
		{
			this.SendPropertyChanging();
			entity.groups = this;
		}
		
		private void detach_user(user entity)
		{
			this.SendPropertyChanging();
			entity.groups = null;
		}
	}
}
#pragma warning restore 1591
