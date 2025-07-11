﻿using System.Windows.Controls.Primitives;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using System.Windows;
using Point = System.Windows.Point;
using System.Collections.Generic;
using System;

namespace Libs.WPF.Helpers
{
    public static class TreeHelper
    {
        public static double GetVisibleWidth(FrameworkElement element, UIElement parent)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));
            Point point = parent != null ? element.TransformToAncestor((Visual)parent).Transform(new Point(0.0, 0.0)) : throw new ArgumentNullException(nameof(parent));
            int num1 = (int)Math.Floor(element.ActualWidth);
            if (TreeHelper.IsAncestorTill(parent.InputHitTest(new Point(point.X + (double)num1, point.Y)) as FrameworkElement, (object)element, (object)parent))
                return (double)num1;
            int num2 = (int)Math.Floor(element.ActualWidth);
            int num3 = 0;
            while (num3 < num2)
            {
                int num4 = (num2 + num3) / 2;
                if (TreeHelper.IsAncestorTill(parent.InputHitTest(new Point(point.X + (double)num4, point.Y)) as FrameworkElement, (object)element, (object)parent))
                {
                    if (!TreeHelper.IsAncestorTill(parent.InputHitTest(new Point(point.X + (double)num4 + 1.0, point.Y)) as FrameworkElement, (object)element, (object)parent))
                        return (double)num4;
                    num3 = num4;
                }
                else
                    num2 = num4;
            }
            return element.ActualWidth;
        }
        private static bool IsAncestorTill(FrameworkElement element, object ancestor, object container)
        {
            if (element == null) return false;
            FrameworkElement frameworkElement = element;
            while (frameworkElement != ancestor)
            {
                if (frameworkElement == container || !((frameworkElement.Parent ?? VisualTreeHelper.GetParent((DependencyObject)frameworkElement)) is FrameworkElement))
                    return false;
            }
            return true;
        }
        /// <summary>
        /// Finds a parent of a given item on the visual tree.
        /// </summary>
        /// <typeparam name="T">The type of the queried item.</typeparam>
        /// <param name="child">A direct or indirect child of the
        /// queried item.</param>
        /// <returns>The first parent item that matches the submitted
        /// type parameter. If not matching item can be found, a null
        /// reference is being returned.</returns>
        public static T TryFindParent<T>(this DependencyObject child)
            where T : DependencyObject
        {
            // get parent item
            DependencyObject parentObject = GetParentObject(child);

            // we've reached the end of the tree
            if (parentObject == null)
            {
                return null;
            }

            //check if the parent matches the type we're looking for
            T parent = parentObject as T;
            return parent ?? TryFindParent<T>(parentObject);
        }

        /// <summary>
        /// Finds all Ancestors of a given item on the visual tree.
        /// </summary>
        /// <param name="child">A node in a visual tree</param>
        /// <returns>All ancestors in visual tree of <paramref name="child"/> element</returns>
        public static IEnumerable<DependencyObject> GetAncestors(this DependencyObject child)
        {
            DependencyObject parent = VisualTreeHelper.GetParent(child);
            while (parent != null)
            {
                yield return parent;
                parent = VisualTreeHelper.GetParent(parent);
            }
        }

        /// <summary>
        /// Finds a Child of a given item in the visual tree. 
        /// </summary>
        /// <param name="parent">A direct parent of the queried item.</param>
        /// <typeparam name="T">The type of the queried item.</typeparam>
        /// <param name="childName">x:Name or Name of child. </param>
        /// <returns>The first parent item that matches the submitted type parameter. 
        /// If not matching item can be found, 
        /// a null parent is being returned.</returns>
        public static T FindChild<T>(this DependencyObject parent, string childName = null)
            where T : DependencyObject
        {
            // Confirm parent and childName are valid. 
            if (parent == null)
            {
                return null;
            }

            T foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                // If the child is not of the request child type child
                T childType = child as T;
                if (childType == null)
                {
                    // recursively drill down the tree
                    foundChild = FindChild<T>(child, childName);
                    // If the child is found, break so we do not overwrite the found child. 
                    if (foundChild != null)
                    {
                        break;
                    }
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    // If the child's name is set for search
                    if (child is IFrameworkInputElement frameworkInputElement && frameworkInputElement.Name == childName)
                    {
                        // if the child's name is of the request name
                        foundChild = (T)child;
                        break;
                    }
                    else
                    {
                        // recursively drill down the tree
                        foundChild = FindChild<T>(child, childName);
                        // If the child is found, break so we do not overwrite the found child. 
                        if (foundChild != null)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    // child element found.
                    foundChild = (T)child;
                    break;
                }
            }

            return foundChild;
        }

        /// <summary>
        /// This method is an alternative to WPF's
        /// <see cref="VisualTreeHelper.GetParent"/> method, which also supports content elements. Keep in mind that for content element,
        /// this method falls back to the logical tree of the element!
        /// </summary>
        /// <param name="child">The item to be processed.</param>
        /// <returns>The submitted item's parent, if available. Otherwise
        /// null.</returns>
        public static DependencyObject GetParentObject(this DependencyObject child)
        {
            if (child == null)
            {
                return null;
            }

            // handle content elements separately
            if (child is ContentElement contentElement)
            {
                DependencyObject parent = ContentOperations.GetParent(contentElement);
                if (parent != null)
                {
                    return parent;
                }

                return contentElement is FrameworkContentElement fce ? fce.Parent : null;
            }

            DependencyObject childParent = VisualTreeHelper.GetParent(child);
            if (childParent != null)
            {
                return childParent;
            }

            // also try searching for parent in framework elements (such as DockPanel, etc)
            if (child is FrameworkElement frameworkElement)
            {
                DependencyObject parent = frameworkElement.Parent;
                if (parent != null)
                {
                    return parent;
                }
            }

            return null;
        }

        /// <summary>
        /// Analyzes both visual and logical tree in order to find all elements of a given
        /// type that are descendants of the <paramref name="source"/> item.
        /// </summary>
        /// <typeparam name="T">The type of the queried items.</typeparam>
        /// <param name="source">The root element that marks the source of the search. If the
        /// source is already of the requested type, it will not be included in the result.</param>
        /// <param name="forceUsingTheVisualTreeHelper">Sometimes it's better to search in the VisualTree (e.g. in tests)</param>
        /// <returns>All descendants of <paramref name="source"/> that match the requested type.</returns>
        public static IEnumerable<T> FindChildren<T>(this DependencyObject source, bool forceUsingTheVisualTreeHelper = false)
            where T : DependencyObject
        {
            if (source != null)
            {
                IEnumerable<DependencyObject> childs = GetChildObjects(source, forceUsingTheVisualTreeHelper);
                foreach (DependencyObject child in childs)
                {
                    //analyze if children match the requested type
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    //recurse tree
                    foreach (T descendant in FindChildren<T>(child, forceUsingTheVisualTreeHelper))
                    {
                        yield return descendant;
                    }
                }
            }
        }

        /// <summary>
        /// This method is an alternative to WPF's
        /// <see cref="VisualTreeHelper.GetChild"/> method, which also
        /// supports content elements. Keep in mind that for content elements,
        /// this method falls back to the logical tree of the element.
        /// </summary>
        /// <param name="parent">The item to be processed.</param>
        /// <param name="forceUsingTheVisualTreeHelper">Sometimes it's better to search in the VisualTree (e.g. in tests)</param>
        /// <returns>The submitted item's child elements, if available.</returns>
        public static IEnumerable<DependencyObject> GetChildObjects(this DependencyObject parent, bool forceUsingTheVisualTreeHelper = false)
        {
            if (parent == null)
            {
                yield break;
            }

            if (!forceUsingTheVisualTreeHelper && (parent is ContentElement || parent is FrameworkElement))
            {
                //use the logical tree for content / framework elements
                foreach (object obj in LogicalTreeHelper.GetChildren(parent))
                {
                    if (obj is DependencyObject dependencyObject)
                    {
                        yield return dependencyObject;
                    }
                }
            }
            else if (parent is Visual || parent is Visual3D)
            {
                //use the visual tree per default
                int count = VisualTreeHelper.GetChildrenCount(parent);
                for (int i = 0; i < count; i++)
                {
                    yield return VisualTreeHelper.GetChild(parent, i);
                }
            }
        }

        /// <summary>
        /// Tries to locate a given item within the visual tree,
        /// starting with the dependency object at a given position. 
        /// </summary>
        /// <typeparam name="T">The type of the element to be found
        /// on the visual tree of the element at the given location.</typeparam>
        /// <param name="reference">The main element which is used to perform
        /// hit testing.</param>
        /// <param name="point">The position to be evaluated on the origin.</param>
        public static T TryFindFromPoint<T>(UIElement reference, Point point)
            where T : DependencyObject
        {
            if (!(reference.InputHitTest(point) is DependencyObject element))
            {
                return null;
            }

            if (element is T theObject)
            {
                return theObject;
            }

            return TryFindParent<T>(element);
        }

        public static bool IsDescendantOf(this DependencyObject node, DependencyObject reference)
        {
            bool success = false;

            DependencyObject curr = node;

            while (curr != null)
            {
                if (curr == reference)
                {
                    success = true;
                    break;
                }

                if (curr is Popup popup)
                {
                    curr = popup;

                    if (popup != null)
                    {
                        // Try the poup Parent
                        curr = popup.Parent;

                        // Otherwise fall back to placement target
                        if (curr == null)
                        {
                            curr = popup.PlacementTarget;
                        }
                    }
                }
                else // Otherwise walk tree
                {
                    curr = curr.GetParentObject();
                }
            }

            return success;
        }
    }
}