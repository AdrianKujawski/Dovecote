// -----------------------------------------------------------------------
// <copyright file="UiHelper.cs">
//     Copyright (c) 2017, Adrian Kujawski.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Dovecote.Model {

	static class UiHelper {
		public static T FindChildByName<T>(DependencyObject parent, string childName)
			where T : DependencyObject {
			if (parent == null) return null;

			T foundChild = null;

			var childrenCount = VisualTreeHelper.GetChildrenCount(parent);
			for (var i = 0; i < childrenCount; i++) {
				var child = VisualTreeHelper.GetChild(parent, i);
				var childType = child as T;
				if (childType == null) {
					foundChild = FindChildByName<T>(child, childName);

					if (foundChild != null) break;
				}
				else if (!string.IsNullOrEmpty(childName)) {
					var frameworkElement = child as FrameworkElement;
					if (frameworkElement == null || frameworkElement.Name != childName) continue;

					foundChild = (T)child;
					break;
				}
				else {
					foundChild = (T)child;
					break;
				}
			}

			return foundChild;
		}

		public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj)
			where T : DependencyObject {
			if (depObj == null) yield break;

			for (var i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++) {
				var child = VisualTreeHelper.GetChild(depObj, i);
				var children = child as T;
				if (children != null) {
					yield return children;
				}

				foreach (var childOfChild in FindVisualChildren<T>(child)) {
					yield return childOfChild;
				}
			}
		}
	}

}
