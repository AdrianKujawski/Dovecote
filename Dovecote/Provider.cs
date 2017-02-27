// -----------------------------------------------------------------------
// <copyright file="Provider.cs">
//     Copyright (c) 2017, Adrian Kujawski.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;

namespace Dovecote {

	static class Provider {
		public delegate void RefreshData();


		static HodowlaEntities1 _entity;

		static HodowlaEntities1 Entity {
			get {
				if (_entity != null) return _entity;

				_entity = new HodowlaEntities1();
				return _entity;
			}
		}

		public static Result Add<T>(T dbSet) {
			try {
				var type = dbSet.GetType();

				if (type == typeof(Color)) {
					Entity.Color.Add((Color)(object)dbSet);
				}
				else if (type == typeof(EyeColor)) {
					Entity.EyeColor.Add((EyeColor)(object)dbSet);
				}
				else if (type == typeof(Gender)) {
					Entity.Gender.Add((Gender)(object)dbSet);
				}
				else if (type == typeof(Dovecote)) {
					Entity.Dovecote.Add((Dovecote)(object)dbSet);
				}
				else if (type == typeof(Line)) {
					Entity.Line.Add((Line)(object)dbSet);
				}
				else if (type == typeof(Pigeon)) {
					Entity.Pigeon.Add((Pigeon)(object)dbSet);
				}
				else if (type == typeof(Race)) {
					Entity.Race.Add((Race)(object)dbSet);
				}
				else if (type == typeof(Yearbook)) {
					Entity.Yearbook.Add((Yearbook)(object)dbSet);
				}
				else if (type == typeof(Category)) {
					Entity.Category.Add((Category)(object)dbSet);
				}
				else if (type == typeof(Status)) {
					Entity.Status.Add((Status)(object)dbSet);
				}

				return SaveChanges<T>(type);
			}
			catch (Exception exception) {
				MessageBox.Show(exception.Message + Environment.NewLine + exception.InnerException, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
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
				else if (type == typeof(Category)) {
					var value = (Category)(object)dbSet;
					Entity.Category.Add(value);
				}
				else if (type == typeof(Status)) {
					var value = (Status)(object)dbSet;
					Entity.Status.Add(value);
				}

				return SaveChanges<T>(type);
			}
			catch (Exception exception) {
				MessageBox.Show(exception.Message + Environment.NewLine + exception.InnerException, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
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

				if (type == typeof(Category)) {
					return Entity.Category.ToList();
				}

				if (type == typeof(Status)) {
					return Entity.Status.ToList();
				}
			}
			catch (Exception exception) {
				MessageBox.Show(exception.Message + Environment.NewLine + exception.InnerException, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
			}

			MessageBox.Show($"Brak typu {type}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
			return null;
		}

		public static Result EditPigeon(Pigeon pigeon, long? editedPigeonId) {
			var origin = Entity.Pigeon.Find(editedPigeonId);
			if (origin == null) throw new Exception("Nie znaleziono edytowanego gołebia w bazie danych.");

			if (editedPigeonId != null) pigeon.Id = (long)editedPigeonId;
			Entity.Entry(origin).CurrentValues.SetValues(pigeon);
			if (SaveChanges() == Result.Success)
				return Result.Success;

			Entity.Database.BeginTransaction().Rollback();
			throw new Exception("Rekord nie został zmieniony.");
		}

		static Result SaveChanges() {
			Entity.SaveChanges();

			DataChanged?.Invoke();
			return Result.Success;
		}

		static Result SaveChanges<T>(Type type) {
			var result = 0;
			try {
				result = Entity.SaveChanges();
			}
			catch (Exception) {
				UndoChanges();
			}
			if (result == 0)
				throw new Exception($"Brak cechy {type}");

			DataChanged?.Invoke();
			return Result.Success;
		}

		static void UndoChanges() {
			foreach (var entry in Entity.ChangeTracker.Entries()) {
				switch (entry.State) {
					case EntityState.Modified:
						entry.State = EntityState.Unchanged;
						break;
					case EntityState.Added:
						entry.State = EntityState.Detached;
						break;
					case EntityState.Deleted:
						entry.Reload();
						break;
				}
			}
		}

		public static event RefreshData DataChanged;
	}

}
