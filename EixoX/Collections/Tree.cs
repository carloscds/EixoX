﻿using System;
using System.Collections.Generic;
using System.Text;
/*
 * First Author: Rodrigo Portela (rodrigo.portela@gmail.com) in 2013-01-01
 */
namespace EixoX.Collections
{
    /// <summary>
    /// Represents a nice tree data structure.
    /// </summary>
    /// <typeparam name="T">The type of value stored on the nodes.</typeparam>
    public class Tree<T>
        : LinkedList<TreeNode<T>>
    {

        /// <summary>
        /// Constructs a new tree.
        /// </summary>
        public Tree() { }
        /// <summary>
        /// Constructs a new tree based on an enumeration of nodes.
        /// </summary>
        /// <param name="nodes">The node enumeration.</param>
        public Tree(IEnumerable<T> nodes)
        {
            foreach (T item in nodes)
                base.AddLast(new TreeNode<T>(item));
        }

        /// <summary>
        /// Does a depth search based on a comparer.
        /// </summary>
        /// <param name="value">The value to look for.</param>
        /// <param name="comparer">The comparer to use.</param>
        /// <returns>The first tree node found.</returns>
        public TreeNode<T> DepthSearch(T value, IEqualityComparer<T> comparer)
        {
            TreeNode<T> found = null;
            for (LinkedListNode<TreeNode<T>> node = First; node != null && found != null; node = node.Next)
                found = comparer.Equals(value, node.Value.Value) ?
                    node.Value :
                    node.Value.DepthSearch(value, comparer);

            return found;
        }

        /// <summary>
        /// Does a depth search based on a comparer.
        /// </summary>
        /// <param name="value">The value to look for.</param>
        /// <returns>The first tree node found.</returns>
        public TreeNode<T> DepthSearch(T value)
        {
            return DepthSearch(value, new HashcodeComparer<T>());
        }

        /// <summary>
        /// Does a depth search based on a comparer.
        /// </summary>
        /// <param name="value">The value to look for.</param>
        /// <param name="func">The delegate fuction to use.</param>
        /// <returns>The first tree node found.</returns>
        public TreeNode<T> DepthSearch(T value, EqualsToHanlder<T> func)
        {
            TreeNode<T> found = null;
            for (LinkedListNode<TreeNode<T>> node = First; node != null && found != null; node = node.Next)
                found = func.Invoke(value, node.Value.Value) ?
                    node.Value :
                    node.Value.DepthSearch(value, func);

            return found;
        }
    }
}