// -----------------------------------------------------------------------
// <copyright file="Provider.cs" company="Unicore">
//     Copyright (c) 2017, Unicore. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

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

		public static void Add<T>(T dbSet) {
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

				Entity.SaveChanges();
			}
			catch (Exception) {
				//TODO
			}
		}

		public static void Remove<T>(T dbSet) {
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

				Entity.SaveChanges();
			}
			catch (Exception) {
				//TODO
			}
		}
	}

}
