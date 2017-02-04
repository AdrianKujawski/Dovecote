// -----------------------------------------------------------------------
// <copyright file="Provider.cs" company="Unicore">
//     Copyright (c) 2017, Unicore. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Documents;

namespace Dovecote {

	static class Provider {
		static HodowlaEntities _entity;

		static HodowlaEntities Entity {
			get {
				if (_entity != null) return _entity;

				_entity = new HodowlaEntities();
				return _entity;
			}
		}

		public static Result Add<T>(T dbSet) {
			try {
				var type = dbSet.GetType();

				if (type == typeof(Color)) {
					var value = (Color)(object)dbSet;
					Entity.Color.Add(value);
				}
				else if (type == typeof(EyeColor)) {
					var value = (EyeColor)(object)dbSet;
					Entity.EyeColor.Add(value);
				}
				else if (type == typeof(Gender)) {
					var value = (Gender)(object)dbSet;
					Entity.Gender.Add(value);
				}
				else if (type == typeof(Dovecote)) {
					var value = (Dovecote)(object)dbSet;
					Entity.Dovecote.Add(value);
				}
				else if (type == typeof(Line)) {
					var value = (Line)(object)dbSet;
					Entity.Line.Add(value);
				}
				else if (type == typeof(Pigeon)) {
					var value = (Pigeon)(object)dbSet;
					Entity.Pigeon.Add(value);
				}
				else if (type == typeof(Race)) {
					var value = (Race)(object)dbSet;
					Entity.Race.Add(value);
				}
				else if (type == typeof(Yearbook)) {
					var value = (Yearbook)(object)dbSet;
					Entity.Yearbook.Add(value);
				}


				return SaveChanges<T>(type);
			}
			catch (Exception exception) {
				MessageBox.Show(exception.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
				return Result.Error;
			}
		}

		public static Result Remove<T>(T dbSet) {
			try {
				var type = dbSet.GetType();

				if (type == typeof(Color)) {
					var value = (Color)(object)dbSet;
					Entity.Color.Remove(value);
				}
				else if (type == typeof(EyeColor)) {
					var value = (EyeColor)(object)dbSet;
					Entity.EyeColor.Remove(value);
				}
				else if (type == typeof(Gender)) {
					var value = (Gender)(object)dbSet;
					Entity.Gender.Remove(value);
				}
				else if (type == typeof(Dovecote)) {
					var value = (Dovecote)(object)dbSet;
					Entity.Dovecote.Remove(value);
				}
				else if (type == typeof(Line)) {
					var value = (Line)(object)dbSet;
					Entity.Line.Remove(value);
				}
				else if (type == typeof(Pigeon)) {
					var value = (Pigeon)(object)dbSet;
					Entity.Pigeon.Remove(value);
				}
				else if (type == typeof(Race)) {
					var value = (Race)(object)dbSet;
					Entity.Race.Remove(value);
				}

				return SaveChanges<T>(type);
			}
			catch (Exception exception) {
				MessageBox.Show(exception.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
				return Result.Error;
			}
		}

		public static IEnumerable GetList<T>(Type type) {
			try {
				if (type == typeof(Color)) {
					return Entity.Color.ToList();
				}

				if (type == typeof(EyeColor)) {
					return Entity.EyeColor.ToList();
				}

				if (type == typeof(Gender)) {
					return Entity.Gender.ToList();
				}

				if (type == typeof(Dovecote)) {
					return Entity.Dovecote.ToList();
				}

				if (type == typeof(Line)) {
					return Entity.Line.ToList();
				}

				if (type == typeof(Pigeon)) {
					return Entity.Pigeon.ToList();
				}

				if (type == typeof(Race)) {
					return Entity.Race.ToList();
				}

				if (type == typeof(Yearbook)) {
					return Entity.Yearbook.ToList();
				}

			}
			catch (Exception exception) {
				MessageBox.Show(exception.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
			}

			MessageBox.Show($"Brak typu {type}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
			return null;

		}

		static Result SaveChanges<T>(Type type) {
			var result = Entity.SaveChanges();
			if (result == 0)
				throw new Exception($"Brak cechy {type}");

			DataChanged?.Invoke();
			return Result.Success;
		}


		public delegate void RefreshData();
		public static event RefreshData DataChanged;
	}

}
