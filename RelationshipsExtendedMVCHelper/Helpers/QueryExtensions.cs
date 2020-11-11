﻿using CMS.DataEngine;
using CMS.DocumentEngine;
using CMS.Helpers;
using CMS.Relationships;
using RelationshipsExtended.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace RelationshipsExtended
{
    /// <summary>
    /// 
    /// </summary>
    public static class QueryExtensions
    {
        #region "Related Pages"

        /// <summary>
        /// Filters documents by those in relationship to the given Node using Related Pages.
        /// </summary>
        /// <param name="nodeGuid">The NodeGuid that the documents must be related to</param>
        /// <param name="relationshipName">Name of the relationship. If not provided documents from all relationships will be retrieved.</param>
        /// <param name="Ordered">Order the documents based on this relationship.</param>
        /// <param name="AscendingOrder">If the ordering should be ascending or not</param>
        /// <param name="ReverseRelationship">If true, then will filter documents that have the given node as a relationship, vs. filtering documents that are related to the given node.  Ordering is disabled if reversed</param>
        public static DocumentQuery InRelationWithOrder(this DocumentQuery baseQuery, Guid nodeGuid, string relationshipName = null, bool Ordered = true, bool AscendingOrder = true, bool ReverseRelationship = false)
        {
            // Get the RelationshipID and NodeID
            int? NodeID = GetNodeID(nodeGuid);
            if (!NodeID.HasValue)
            {
                return baseQuery;
            }
            return InRelationWithOrderInternal(baseQuery, NodeID.Value, GetRelationshipNameID(relationshipName), Ordered, AscendingOrder, ReverseRelationship, ReverseRelationship);
        }


        /// <summary>
        /// Filters documents by those in relationship to the given Node using Related Pages.
        /// </summary>
        /// <param name="nodeID">The NodeID</param>
        /// <param name="relationshipName">Name of the relationship. If not provided documents from all relationships will be retrieved.</param>
        /// <param name="Ordered">Order the documents based on this relationship.</param>
        /// <param name="AscendingOrder">If the ordering should be ascending or not</param>
        /// <param name="ReverseRelationship">If true, then will filter documents that have the given node as a relationship, vs. filtering documents that are related to the given node.  Ordering is disabled if reversed</param>
        public static DocumentQuery InRelationWithOrder(this DocumentQuery baseQuery, int nodeID, string relationshipName = null, bool Ordered = true, bool AscendingOrder = true, bool ReverseRelationship = false)
        {
            // Get the RelationshipID and NodeID
            return InRelationWithOrderInternal(baseQuery, nodeID, GetRelationshipNameID(relationshipName), Ordered, AscendingOrder, ReverseRelationship);
        }


        /// <summary>
        /// Filters documents by those in relationship to the given Node using Related Pages.
        /// </summary>
        /// <param name="nodeGuid">The NodeGuid</param>
        /// <param name="relationshipName">Name of the relationship. If not provided documents from all relationships will be retrieved.</param>
        /// <param name="Ordered">Order the documents based on this relationship.</param>
        /// <param name="AscendingOrder">If the ordering should be ascending or not</param>
        /// <param name="ReverseRelationship">If true, then will filter documents that have the given node as a relationship, vs. filtering documents that are related to the given node.  Ordering is disabled if reversed</param>
        public static DocumentQuery<TDocument> InRelationWithOrder<TDocument>(this DocumentQuery<TDocument> baseQuery, Guid nodeGuid, string relationshipName = null, bool Ordered = true, bool AscendingOrder = true, bool ReverseRelationship = false) where TDocument : TreeNode, new()
        {
            // Get the RelationshipID and NodeID
            int? NodeID = GetNodeID(nodeGuid);
            if (!NodeID.HasValue)
            {
                return baseQuery;
            }

            return InRelationWithOrderInternal(baseQuery, NodeID.Value, GetRelationshipNameID(relationshipName), Ordered, AscendingOrder, ReverseRelationship);
        }


        /// <summary>
        /// Filters documents by those in relationship to the given Node using Related Pages.
        /// </summary>
        /// <param name="nodeID">The NodeID</param>
        /// <param name="relationshipName">Name of the relationship. If not provided documents from all relationships will be retrieved.</param>
        /// <param name="Ordered">Order the documents based on this relationship.</param>
        /// <param name="AscendingOrder">If the ordering should be ascending or not</param>
        /// <param name="ReverseRelationship">If true, then will filter documents that have the given node as a relationship, vs. filtering documents that are related to the given node.  Ordering is disabled if reversed</param>
        public static DocumentQuery<TDocument> InRelationWithOrder<TDocument>(this DocumentQuery<TDocument> baseQuery, int nodeID, string relationshipName = null, bool Ordered = true, bool AscendingOrder = true, bool ReverseRelationship = false) where TDocument : TreeNode, new()
        {
            return InRelationWithOrderInternal(baseQuery, nodeID, GetRelationshipNameID(relationshipName), Ordered, AscendingOrder, ReverseRelationship);
        }


        /// <summary>
        /// Filters documents by those in relationship to the given Node using Related Pages.
        /// </summary>
        /// <param name="baseQuery">The Base Document Query</param>
        /// <param name="nodeGuid">The NodeGuid</param>
        /// <param name="relationshipName">Name of the relationship. If not provided documents from all relationships will be retrieved.</param>
        /// <param name="Ordered">Order the documents based on this relationship.</param>
        /// <param name="AscendingOrder">If the ordering should be ascending or not</param>
        /// <param name="ReverseRelationship">If true, then will filter documents that have the given node as a relationship, vs. filtering documents that are related to the given node.  Ordering is disabled if reversed</param>
        public static MultiDocumentQuery InRelationWithOrder(this MultiDocumentQuery baseQuery, Guid nodeGuid, string relationshipName = null, bool Ordered = true, bool AscendingOrder = true, bool ReverseRelationship = false)
        {
            // Get the RelationshipID and NodeID
            int? NodeID = GetNodeID(nodeGuid);
            if (!NodeID.HasValue)
            {
                return baseQuery;
            }
            return InRelationWithOrderInternal(baseQuery, NodeID.Value, GetRelationshipNameID(relationshipName), Ordered, AscendingOrder, ReverseRelationship);
        }


        /// <summary>
        /// Filters documents by those in relationship to the given Node using Related Pages.
        /// </summary>
        /// <param name="nodeID">The NodeID</param>
        /// <param name="relationshipName">Name of the relationship. If not provided documents from all relationships will be retrieved.</param>
        /// <param name="Ordered">Order the documents based on this relationship.</param>
        /// <param name="AscendingOrder">If the ordering should be ascending or not</param>
        /// <param name="ReverseRelationship">If true, then will filter documents that have the given node as a relationship, vs. filtering documents that are related to the given node.  Ordering is disabled if reversed</param>
        public static MultiDocumentQuery InRelationWithOrder(this MultiDocumentQuery baseQuery, int nodeID, string relationshipName = null, bool Ordered = true, bool AscendingOrder = true, bool ReverseRelationship = false)
        {
            // Get the RelationshipID and NodeID
            return InRelationWithOrderInternal(baseQuery, nodeID, GetRelationshipNameID(relationshipName), Ordered, AscendingOrder, ReverseRelationship);
        }

        #endregion

        #region "Custom Relationships"

        /// <summary>
        /// Filters documents by those in relationship to the given object using a custom binding class.
        /// </summary>        
        /// <param name="BindingClass">The Binding Class Code Name Ex: Demo.BarNode</param>
        /// <param name="PrimaryClass">The primary class you are binding to Ex: Demo.Bar</param>
        /// <param name="InRelationshipWithValue">The value of the primary class that is used for the relationship lookup. Ex: 'BarA'</param>
        /// <param name="ObjectIDFieldName">The Field Name of this object that matches the binding table's Left Field value. Ex: BarID</param>
        /// <param name="LeftFieldName">The Field Name of the binding class that contains the Document identity value. Ex: BarNodeNodeID (from Demo.BarNode)</param>
        /// <param name="RightFieldName">The Field Name of the binding class that contains the related objects's identity value.  Ex: BarNodeBarID (from Demo.BarNode)</param>
        /// <param name="OrderColumn">The Order column name, if empty then will not order</param>
        /// <param name="OrderAsc">If the ordering should be done Ascending or Descending</param>
        /// <param name="Identity">RelEnums.IdentityType of what value is stored in the binding table for the Node, default is ID</param>
        /// <returns></returns>
        public static DocumentQuery InCustomRelationshipWithOrder(DocumentQuery baseQuery, string BindingClass, string PrimaryClass, object InRelationshipWithValue, string ObjectIDFieldName, string LeftFieldName, string RightFieldName, string OrderColumn = null, bool OrderAsc = true, IdentityType Identity = IdentityType.ID)
        {
            baseQuery.Source((QuerySource s) => s.InnerJoin(new QuerySourceTable(RelHelper.GetClassObjSummary(BindingClass).TableName), new WhereCondition($"{RelHelper.GetBracketedColumnName(ObjectIDFieldName)} = {RelHelper.GetBracketedColumnName(RightFieldName)}").WhereEquals(LeftFieldName, GetLookupValue(PrimaryClass, InRelationshipWithValue, Identity))));

            // add the order by
            if (!string.IsNullOrWhiteSpace(OrderColumn))
            {
                if (OrderAsc)
                {
                    baseQuery.OrderBy(OrderColumn);
                }
                else
                {
                    baseQuery.OrderByDescending(OrderColumn);
                }
            }
            return baseQuery;
        }

        /// <summary>
        /// Filters documents by those in relationship to the given object using a custom binding class.
        /// </summary>        
        /// <param name="BindingClass">The Binding Class Code Name Ex: Demo.BarNode</param>
        /// <param name="PrimaryClass">The primary class you are binding to Ex: Demo.Bar</param>
        /// <param name="InRelationshipWithValue">The value of the primary class that is used for the relationship lookup. Ex: 'BarA'</param>
        /// <param name="ObjectIDFieldName">The Field Name of this object that matches the binding table's Left Field value. Ex: BarID</param>
        /// <param name="LeftFieldName">The Field Name of the binding class that contains the Document identity value. Ex: BarNodeNodeID (from Demo.BarNode)</param>
        /// <param name="RightFieldName">The Field Name of the binding class that contains the related objects's identity value.  Ex: BarNodeBarID (from Demo.BarNode)</param>
        /// <param name="OrderColumn">The Order column name, if empty then will not order</param>
        /// <param name="OrderAsc">If the ordering should be done Ascending or Descending</param>
        /// <param name="Identity">RelEnums.IdentityType of what value is stored in the binding table for the Node, default is ID</param>
        public static DocumentQuery<TDocument> InCustomRelationshipWithOrder<TDocument>(DocumentQuery<TDocument> baseQuery, string BindingClass, string PrimaryClass, object InRelationshipWithValue, string ObjectIDFieldName, string LeftFieldName, string RightFieldName, string OrderColumn = null, bool OrderAsc = true, IdentityType Identity = IdentityType.ID) where TDocument : TreeNode, new()
        {
            baseQuery.Source((QuerySource s) => s.InnerJoin(new QuerySourceTable(RelHelper.GetClassObjSummary(BindingClass).TableName), new WhereCondition($"{RelHelper.GetBracketedColumnName(ObjectIDFieldName)} = {RelHelper.GetBracketedColumnName(RightFieldName)}").WhereEquals(LeftFieldName, GetLookupValue(PrimaryClass, InRelationshipWithValue, Identity))));

            // add the order by
            if (!string.IsNullOrWhiteSpace(OrderColumn))
            {
                if (OrderAsc)
                {
                    baseQuery.OrderBy(OrderColumn);
                }
                else
                {
                    baseQuery.OrderByDescending(OrderColumn);
                }
            }
            return baseQuery;
        }

        /// <summary>
        /// Filters documents by those in relationship to the given object using a custom binding class.
        /// </summary>        
        /// <param name="BindingClass">The Binding Class Code Name Ex: Demo.BarNode</param>
        /// <param name="PrimaryClass">The primary class you are binding to Ex: Demo.Bar</param>
        /// <param name="InRelationshipWithValue">The value of the primary class that is used for the relationship lookup. Ex: 'BarA'</param>
        /// <param name="ObjectIDFieldName">The Field Name of this object that matches the binding table's Left Field value. Ex: BarID</param>
        /// <param name="LeftFieldName">The Field Name of the binding class that contains the Document identity value. Ex: BarNodeNodeID (from Demo.BarNode)</param>
        /// <param name="RightFieldName">The Field Name of the binding class that contains the related objects's identity value.  Ex: BarNodeBarID (from Demo.BarNode)</param>
        /// <param name="OrderColumn">The Order column name, if empty then will not order</param>
        /// <param name="OrderAsc">If the ordering should be done Ascending or Descending</param>
        /// <param name="Identity">RelEnums.IdentityType of what value is stored in the binding table for the Node, default is ID</param>
        public static MultiDocumentQuery InCustomRelationshipWithOrder(MultiDocumentQuery baseQuery, string BindingClass, string PrimaryClass, object InRelationshipWithValue, string ObjectIDFieldName, string LeftFieldName, string RightFieldName, string OrderColumn = null, bool OrderAsc = true, IdentityType Identity = IdentityType.ID)
        {
            baseQuery.Source((QuerySource s) => s.InnerJoin(new QuerySourceTable(RelHelper.GetClassObjSummary(BindingClass).TableName), new WhereCondition($"{RelHelper.GetBracketedColumnName(ObjectIDFieldName)} = {RelHelper.GetBracketedColumnName(RightFieldName)}").WhereEquals(LeftFieldName, GetLookupValue(PrimaryClass, InRelationshipWithValue, Identity))));

            // add the order by
            if (!string.IsNullOrWhiteSpace(OrderColumn))
            {
                if (OrderAsc)
                {
                    baseQuery.OrderBy(OrderColumn);
                }
                else
                {
                    baseQuery.OrderByDescending(OrderColumn);
                }
            }
            return baseQuery;
        }

        /// <summary>
        /// Filters objects by those in relationship to the given other object using a custom binding class.
        /// </summary>        
        /// <example>If you are Querying Bar's that are related to the given Foo object (Demo.FooBar)</example>
        /// <see cref="https://github.com/KenticoDevTrev/RelationshipsExtended/tree/master/Demo/Demo/Classes/ObjectObjectWithOrder"/>
        /// <param name="BindingClass">The Binding Class Code Name Ex: Demo.FooBar</param>
        /// <param name="PrimaryClass">The primary class you are binding to Ex: Demo.Foo</param>
        /// <param name="InRelationshipWithValue">The value of the primary class that is used for the relationship lookup. Ex: 'FooA'</param>
        /// <param name="ObjectIDFieldName">The Field Name of this object that matches the binding table's Left Field value. Ex: FooID</param>
        /// <param name="LeftFieldName">The Field Name of the binding class that contains the the primary class's identity value. Ex: FooBarFooID (from Demo.FooBar)</param>
        /// <param name="RightFieldName">The Field Name of the binding class that contains the related objects's identity value.  Ex: FooBarBarID (from Demo.FooBar)</param>
        /// <param name="OrderColumn">The Order column name, if empty then will not order. Ex: FooBarOrder</param>
        /// <param name="OrderAsc">If the ordering should be done Ascending or Descending</param>
        /// <param name="Identity">RelEnums.IdentityType of what value is stored in the binding table for the Node, default is ID</param>
        public static ObjectQuery InCustomRelationshipWithOrder(ObjectQuery baseQuery, string BindingClass, string PrimaryClass, object InRelationshipWithValue, string ObjectIDFieldName, string LeftFieldName, string RightFieldName, string OrderColumn = null, bool OrderAsc = true, IdentityType Identity = IdentityType.ID)
        {
            baseQuery.Source((QuerySource s) => s.InnerJoin(new QuerySourceTable(RelHelper.GetClassObjSummary(BindingClass).TableName), new WhereCondition($"{RelHelper.GetBracketedColumnName(ObjectIDFieldName)} = {RelHelper.GetBracketedColumnName(RightFieldName)}").WhereEquals(LeftFieldName, GetLookupValue(PrimaryClass, InRelationshipWithValue, Identity))));

            // add the order by
            if (!string.IsNullOrWhiteSpace(OrderColumn))
            {
                if (OrderAsc)
                {
                    baseQuery.OrderBy(OrderColumn);
                }
                else
                {
                    baseQuery.OrderByDescending(OrderColumn);
                }
            }
            return baseQuery;
        }

        /// <summary>
        /// Filters objects by those in relationship to the given other object using a custom binding class.
        /// </summary>        
        /// <example>If you are Querying Bar's that are related to the given Foo object (Demo.FooBar)</example>
        /// <see cref="https://github.com/KenticoDevTrev/RelationshipsExtended/tree/master/Demo/Demo/Classes/ObjectObjectWithOrder"/>
        /// <param name="BindingClass">The Binding Class Code Name Ex: Demo.FooBar</param>
        /// <param name="PrimaryClass">The primary class you are binding to Ex: Demo.Foo</param>
        /// <param name="InRelationshipWithValue">The value of the primary class that is used for the relationship lookup. Ex: 'FooA'</param>
        /// <param name="ObjectIDFieldName">The Field Name of this object that matches the binding table's Left Field value. Ex: FooID</param>
        /// <param name="LeftFieldName">The Field Name of the binding class that contains the the primary class's identity value. Ex: FooBarFooID (from Demo.FooBar)</param>
        /// <param name="RightFieldName">The Field Name of the binding class that contains the related objects's identity value.  Ex: FooBarBarID (from Demo.FooBar)</param>
        /// <param name="OrderColumn">The Order column name, if empty then will not order. Ex: FooBarOrder</param>
        /// <param name="OrderAsc">If the ordering should be done Ascending or Descending</param>
        /// <param name="Identity">RelEnums.IdentityType of what value is stored in the binding table for the Node, default is ID</param>
        public static ObjectQuery<TObject> InCustomRelationshipWithOrder<TObject>(ObjectQuery<TObject> baseQuery, string BindingClass, object InRelationshipWithValue, string ObjectIDFieldName, string LeftFieldName, string RightFieldName, string OrderColumn = null, bool OrderAsc = true, IdentityType Identity = IdentityType.ID) where TObject : BaseInfo, new()
        {
            baseQuery.Source((QuerySource s) => s.InnerJoin(new QuerySourceTable(RelHelper.GetClassObjSummary(BindingClass).TableName), new WhereCondition($"{RelHelper.GetBracketedColumnName(ObjectIDFieldName)} = {RelHelper.GetBracketedColumnName(RightFieldName)}").WhereEquals(LeftFieldName, GetLookupValue(baseQuery.ObjectType, InRelationshipWithValue, Identity))));

            // add the order by
            if (!string.IsNullOrWhiteSpace(OrderColumn))
            {
                if (OrderAsc)
                {
                    baseQuery.OrderBy(OrderColumn);
                }
                else
                {
                    baseQuery.OrderByDescending(OrderColumn);
                }
            }
            return baseQuery;
        }

        /// <summary>
        /// Filters objects by those in relationship to the given other object using a custom binding class.
        /// </summary>        
        /// <example>If you are Querying Bar's that are related to the given Foo object (Demo.FooBar)</example>
        /// <see cref="https://github.com/KenticoDevTrev/RelationshipsExtended/tree/master/Demo/Demo/Classes/ObjectObjectWithOrder"/>
        /// <param name="BindingClass">The Binding Class Code Name Ex: Demo.FooBar</param>
        /// <param name="PrimaryClass">The primary class you are binding to Ex: Demo.Foo</param>
        /// <param name="InRelationshipWithValue">The value of the primary class that is used for the relationship lookup. Ex: 'FooA'</param>
        /// <param name="ObjectIDFieldName">The Field Name of this object that matches the binding table's Left Field value. Ex: FooID</param>
        /// <param name="LeftFieldName">The Field Name of the binding class that contains the the primary class's identity value. Ex: FooBarFooID (from Demo.FooBar)</param>
        /// <param name="RightFieldName">The Field Name of the binding class that contains the related objects's identity value.  Ex: FooBarBarID (from Demo.FooBar)</param>
        /// <param name="OrderColumn">The Order column name, if empty then will not order. Ex: FooBarOrder</param>
        /// <param name="OrderAsc">If the ordering should be done Ascending or Descending</param>
        /// <param name="Identity">RelEnums.IdentityType of what value is stored in the binding table for the Node, default is ID</param>
        public static MultiObjectQuery InCustomRelationshipWithOrder(MultiObjectQuery baseQuery, string BindingClass, string PrimaryClass, object InRelationshipWithValue, string ObjectIDFieldName, string LeftFieldName, string RightFieldName, string OrderColumn = null, bool OrderAsc = true, IdentityType Identity = IdentityType.ID)
        {
            baseQuery.Source((QuerySource s) => s.InnerJoin(new QuerySourceTable(RelHelper.GetClassObjSummary(BindingClass).TableName), new WhereCondition($"{RelHelper.GetBracketedColumnName(ObjectIDFieldName)} = {RelHelper.GetBracketedColumnName(RightFieldName)}").WhereEquals(LeftFieldName, GetLookupValue(PrimaryClass, InRelationshipWithValue, Identity))));

            // add the order by
            if (!string.IsNullOrWhiteSpace(OrderColumn))
            {
                if (OrderAsc)
                {
                    baseQuery.OrderBy(OrderColumn);
                }
                else
                {
                    baseQuery.OrderByDescending(OrderColumn);
                }
            }
            return baseQuery;
        }

        #endregion

        #region "Document Category Filter"

        /// <summary>
        /// Adds Document Category Condition to the document query.  If no categories given or none found of the given Values, will not apply a true condition (1=1).
        /// </summary>
        /// <param name="Values">list of category values (int IDs, GUIDs, or string CodeNames)</param>
        /// <param name="Condition">RelEnums.ConditionType of what type of condition to generate.</param>
        /// <param name="DocumentIDTableName">The Table Name/Alias where the DocumentID belongs. Only needed for the 'All' Condition, defaults to CMS_Document.</param>
        public static DocumentQuery DocumentCategoryCondition(this DocumentQuery baseQuery, IEnumerable<object> Values, ConditionType Condition = ConditionType.Any, string DocumentIDTableName = "CMS_Document")
        {
            baseQuery.Where(RelHelper.GetDocumentCategoryWhere(Values, Condition, DocumentIDTableName));
            return baseQuery;
        }

        /// <summary>
        /// Adds Document Category Condition to the document query.  If no categories given or none found of the given Values, will not apply a true condition (1=1).
        /// </summary>
        /// <param name="Values">list of category values (int IDs, GUIDs, or string CodeNames)</param>
        /// <param name="Condition">RelEnums.ConditionType of what type of condition to generate.</param>
        /// <param name="DocumentIDTableName">The Table Name/Alias where the DocumentID belongs. Only needed for the 'All' Condition, defaults to CMS_Document.</param>
        public static DocumentQuery<TDocument> DocumentCategoryCondition<TDocument>(DocumentQuery<TDocument> baseQuery, IEnumerable<object> Values, ConditionType Condition = ConditionType.Any, string DocumentIDTableName = "CMS_Document") where TDocument : TreeNode, new()
        {
            baseQuery.Where(RelHelper.GetDocumentCategoryWhere(Values, Condition, DocumentIDTableName));
            return baseQuery;
        }

        /// <summary>
        /// Adds Document Category Condition to the document query.  If no categories given or none found of the given Values, will not apply a true condition (1=1).
        /// </summary>
        /// <param name="Values">list of category values (int IDs, GUIDs, or string CodeNames)</param>
        /// <param name="Condition">RelEnums.ConditionType of what type of condition to generate.</param>
        /// <param name="DocumentIDTableName">The Table Name/Alias where the DocumentID belongs. Only needed for the 'All' Condition, defaults to CMS_Document.</param>
        public static MultiDocumentQuery DocumentCategoryCondition(this MultiDocumentQuery baseQuery, IEnumerable<object> Values, ConditionType Condition = ConditionType.Any, string DocumentIDTableName = "CMS_Document")
        {
            baseQuery.Where(RelHelper.GetDocumentCategoryWhere(Values, Condition, DocumentIDTableName));
            return baseQuery;
        }

        #endregion

        #region "Node Category Filter"

        /// <summary>
        /// Adds Tree Category Condition to the document query.  If no categories given or none found of the given Values, will not apply a true condition (1=1).
        /// </summary>
        /// <param name="Values">list of category values (int IDs, GUIDs, or string CodeNames)</param>
        /// <param name="Condition">RelEnums.ConditionType of what type of condition to generate.</param>
        /// <param name="DocumentIDTableName">The Table Name/Alias where the DocumentID belongs. Only needed for the 'All' Condition, defaults to CMS_Tree.</param>
        public static DocumentQuery TreeCategoryCondition(this DocumentQuery baseQuery, IEnumerable<object> Values, ConditionType Condition = ConditionType.Any, string DocumentIDTableName = "CMS_Tree")
        {
            baseQuery.Where(RelHelper.GetNodeCategoryWhere(Values, Condition, DocumentIDTableName));
            return baseQuery;
        }

        /// <summary>
        /// Adds Tree Category Condition to the document query.  If no categories given or none found of the given Values, will not apply a true condition (1=1).
        /// </summary>
        /// <param name="Values">list of category values (int IDs, GUIDs, or string CodeNames)</param>
        /// <param name="Condition">RelEnums.ConditionType of what type of condition to generate.</param>
        /// <param name="DocumentIDTableName">The Table Name/Alias where the DocumentID belongs. Only needed for the 'All' Condition, defaults to CMS_Tree.</param>
        public static DocumentQuery<TDocument> TreeCategoryCondition<TDocument>(DocumentQuery<TDocument> baseQuery, IEnumerable<object> Values, ConditionType Condition = ConditionType.Any, string DocumentIDTableName = "CMS_Tree") where TDocument : TreeNode, new()
        {
            baseQuery.Where(RelHelper.GetNodeCategoryWhere(Values, Condition, DocumentIDTableName));
            return baseQuery;
        }

        /// <summary>
        /// Adds Tree Category Condition to the document query.  If no categories given or none found of the given Values, will not apply a true condition (1=1).
        /// </summary>
        /// <param name="Values">list of category values (int IDs, GUIDs, or string CodeNames)</param>
        /// <param name="Condition">RelEnums.ConditionType of what type of condition to generate.</param>
        /// <param name="DocumentIDTableName">The Table Name/Alias where the DocumentID belongs. Only needed for the 'All' Condition, defaults to CMS_Tree.</param>
        public static MultiDocumentQuery TreeCategoryCondition(this MultiDocumentQuery baseQuery, IEnumerable<object> Values, ConditionType Condition = ConditionType.Any, string DocumentIDTableName = "CMS_Tree")
        {
            baseQuery.Where(RelHelper.GetNodeCategoryWhere(Values, Condition, DocumentIDTableName));
            return baseQuery;
        }

        #endregion

        #region "Binding Category Filter"

        /// <summary>
        /// Adds Category Condition (with custom Binding table) to the document query.  If no categories given or none found of the given Values, will not apply a true condition (1=1).
        /// </summary>
        /// <example>If you are retrieving documents that are related to 'region' categories stored in a custom binding table (Demo.NodeRegion)</example>
        /// <see cref="https://github.com/KenticoDevTrev/RelationshipsExtended/tree/master/Demo/Demo/Classes/NodeCategoryCustomTable"/>
        /// <param name="BindingClass">The Binding Class Code Name (ex: Demo.NodeRegion)</param>
        /// <param name="ObjectIDFieldName">The Field Name of this object that matches the binding table's Left Field value. Ex: NodeID, DocumentID</param>
        /// <param name="LeftFieldName">The Field Name of the binding class that contains this Object IDs value.  Ex: NodeRegionNodeID (from Demo.NodeRegion)</param>
        /// <param name="RightFieldName">The Field Name of the binding class that contains the Category's identity value.  Ex: NodeRegionCategoryID (from Demo.NodeRegion) </param>
        /// <param name="Values">list of category values (int IDs, GUIDs, or string CodeNames)</param>
        /// <param name="Identity">RelEnums.IdentityType of what value is stored in the binding table for the category, default is ID</param>
        /// <param name="Condition">RelEnums.ConditionType of what type of condition to generate.</param>
        /// <param name="ObjectIDTableName">The Table Name/Alias where the ObjectIDFieldName belongs. Only needed for the 'All' Condition and if the ObjectIDField and LeftFieldName are the same. Ex: Demo_NodeRegion</param>
        public static DocumentQuery BindingCategoryCondition(this DocumentQuery baseQuery, string BindingClass, string ObjectIDFieldName, string LeftFieldName, string RightFieldName, IEnumerable<object> Values, IdentityType Identity = IdentityType.ID, ConditionType Condition = ConditionType.Any, string ObjectIDTableName = null)
        {
            baseQuery.Where(RelHelper.GetBindingCategoryWhere(BindingClass, ObjectIDFieldName, LeftFieldName, RightFieldName, Values, Identity, Condition, ObjectIDTableName));
            return baseQuery;
        }

        /// <summary>
        /// Adds Category Condition (with custom Binding table) to the document query.  If no categories given or none found of the given Values, will not apply a true condition (1=1).
        /// </summary>
        /// <example>If you are retrieving documents that are related to 'region' categories stored in a custom binding table (Demo.NodeRegion)</example>
        /// <see cref="https://github.com/KenticoDevTrev/RelationshipsExtended/tree/master/Demo/Demo/Classes/NodeCategoryCustomTable"/>
        /// <param name="BindingClass">The Binding Class Code Name (ex: Demo.NodeRegion)</param>
        /// <param name="ObjectIDFieldName">The Field Name of this object that matches the binding table's Left Field value. Ex: NodeID, DocumentID</param>
        /// <param name="LeftFieldName">The Field Name of the binding class that contains this Object IDs value.  Ex: NodeRegionNodeID (from Demo.NodeRegion)</param>
        /// <param name="RightFieldName">The Field Name of the binding class that contains the Category's identity value.  Ex: NodeRegionCategoryID (from Demo.NodeRegion) </param>
        /// <param name="Values">list of category values (int IDs, GUIDs, or string CodeNames)</param>
        /// <param name="Identity">RelEnums.IdentityType of what value is stored in the binding table for the category, default is ID</param>
        /// <param name="Condition">RelEnums.ConditionType of what type of condition to generate.</param>
        /// <param name="ObjectIDTableName">The Table Name/Alias where the ObjectIDFieldName belongs. Only needed for the 'All' Condition and if the ObjectIDField and LeftFieldName are the same. Ex: Demo_NodeRegion</param>
        public static DocumentQuery<TDocument> BindingCategoryCondition<TDocument>(DocumentQuery<TDocument> baseQuery, string BindingClass, string ObjectIDFieldName, string LeftFieldName, string RightFieldName, IEnumerable<object> Values, IdentityType Identity = IdentityType.ID, ConditionType Condition = ConditionType.Any, string ObjectIDTableName = null) where TDocument : TreeNode, new()
        {
            baseQuery.Where(RelHelper.GetBindingCategoryWhere(BindingClass, ObjectIDFieldName, LeftFieldName, RightFieldName, Values, Identity, Condition, ObjectIDTableName));
            return baseQuery;
        }

        /// <summary>
        /// Adds Category Condition (with custom Binding table) to the document query.  If no categories given or none found of the given Values, will not apply a true condition (1=1).
        /// </summary>
        /// <example>If you are retrieving documents that are related to 'region' categories stored in a custom binding table (Demo.NodeRegion)</example>
        /// <see cref="https://github.com/KenticoDevTrev/RelationshipsExtended/tree/master/Demo/Demo/Classes/NodeCategoryCustomTable"/>
        /// <param name="BindingClass">The Binding Class Code Name (ex: Demo.NodeRegion)</param>
        /// <param name="ObjectIDFieldName">The Field Name of this object that matches the binding table's Left Field value. Ex: NodeID, DocumentID</param>
        /// <param name="LeftFieldName">The Field Name of the binding class that contains this Object IDs value.  Ex: NodeRegionNodeID (from Demo.NodeRegion)</param>
        /// <param name="RightFieldName">The Field Name of the binding class that contains the Category's identity value.  Ex: NodeRegionCategoryID (from Demo.NodeRegion) </param>
        /// <param name="Values">list of category values (int IDs, GUIDs, or string CodeNames)</param>
        /// <param name="Identity">RelEnums.IdentityType of what value is stored in the binding table for the category, default is ID</param>
        /// <param name="Condition">RelEnums.ConditionType of what type of condition to generate.</param>
        /// <param name="ObjectIDTableName">The Table Name/Alias where the ObjectIDFieldName belongs. Only needed for the 'All' Condition and if the ObjectIDField and LeftFieldName are the same. Ex: Demo_NodeRegion</param>
        public static MultiDocumentQuery BindingCategoryCondition(this MultiDocumentQuery baseQuery, string BindingClass, string ObjectIDFieldName, string LeftFieldName, string RightFieldName, IEnumerable<object> Values, IdentityType Identity = IdentityType.ID, ConditionType Condition = ConditionType.Any, string ObjectIDTableName = null)
        {
            baseQuery.Where(RelHelper.GetBindingCategoryWhere(BindingClass, ObjectIDFieldName, LeftFieldName, RightFieldName, Values, Identity, Condition, ObjectIDTableName));
            return baseQuery;
        }

        /// <summary>
        /// Adds Category Condition (with custom Binding table) to the object query.  If no categories given or none found of the given Values, will not apply a true condition (1=1).
        /// </summary>
        /// <example>If you are retrieving Foo objects that are related to categories stored in a custom binding table (Demo.FooCategory)</example>
        /// <see cref="https://github.com/KenticoDevTrev/RelationshipsExtended/tree/master/Demo/Demo/Classes/ObjectCategory"/>
        /// <param name="BindingClass">The Binding Class Code Name (ex: Demo.FooCategory)</param>
        /// <param name="ObjectIDFieldName">The Field Name of this object that matches the binding table's Left Field value. Ex: FooID</param>
        /// <param name="LeftFieldName">The Field Name of the binding class that contains this Object IDs value.  Ex: FooCategoryFooID (from Demo.FooCategory)</param>
        /// <param name="RightFieldName">The Field Name of the binding class that contains the Category's identity value.  Ex: FooCategoryCategoryID (from Demo.FooCategory) </param>
        /// <param name="Values">list of category values (int IDs, GUIDs, or string CodeNames)</param>
        /// <param name="Identity">RelEnums.IdentityType of what value is stored in the binding table for the category, default is ID</param>
        /// <param name="Condition">RelEnums.ConditionType of what type of condition to generate.</param>
        /// <param name="ObjectIDTableName">The Table Name/Alias where the ObjectIDFieldName belongs. Only needed for the 'All' Condition and if the ObjectIDField and LeftFieldName are the same. Ex: Demo_FooCategory</param>
        public static ObjectQuery BindingCategoryCondition(this ObjectQuery baseQuery, string BindingClass, string ObjectIDFieldName, string LeftFieldName, string RightFieldName, IEnumerable<object> Values, IdentityType Identity = IdentityType.ID, ConditionType Condition = ConditionType.Any, string ObjectIDTableName = null)
        {
            baseQuery.Where(RelHelper.GetBindingCategoryWhere(BindingClass, ObjectIDFieldName, LeftFieldName, RightFieldName, Values, Identity, Condition, ObjectIDTableName));
            return baseQuery;
        }

        /// <summary>
        /// Adds Category Condition (with custom Binding table) to the object query.  If no categories given or none found of the given Values, will not apply a true condition (1=1).
        /// </summary>
        /// <example>If you are retrieving Foo objects that are related to categories stored in a custom binding table (Demo.FooCategory)</example>
        /// <see cref="https://github.com/KenticoDevTrev/RelationshipsExtended/tree/master/Demo/Demo/Classes/ObjectCategory"/>
        /// <param name="BindingClass">The Binding Class Code Name (ex: Demo.FooCategory)</param>
        /// <param name="ObjectIDFieldName">The Field Name of this object that matches the binding table's Left Field value. Ex: FooID</param>
        /// <param name="LeftFieldName">The Field Name of the binding class that contains this Object IDs value.  Ex: FooCategoryFooID (from Demo.FooCategory)</param>
        /// <param name="RightFieldName">The Field Name of the binding class that contains the Category's identity value.  Ex: FooCategoryCategoryID (from Demo.FooCategory) </param>
        /// <param name="Values">list of category values (int IDs, GUIDs, or string CodeNames)</param>
        /// <param name="Identity">RelEnums.IdentityType of what value is stored in the binding table for the category, default is ID</param>
        /// <param name="Condition">RelEnums.ConditionType of what type of condition to generate.</param>
        /// <param name="ObjectIDTableName">The Table Name/Alias where the ObjectIDFieldName belongs. Only needed for the 'All' Condition and if the ObjectIDField and LeftFieldName are the same. Ex: Demo_FooCategory</param>
        public static ObjectQuery<TObject> BindingCategoryCondition<TObject>(ObjectQuery<TObject> baseQuery, string BindingClass, string ObjectIDFieldName, string LeftFieldName, string RightFieldName, IEnumerable<object> Values, IdentityType Identity = IdentityType.ID, ConditionType Condition = ConditionType.Any, string ObjectIDTableName = null) where TObject : BaseInfo, new()
        {
            baseQuery.Where(RelHelper.GetBindingCategoryWhere(BindingClass, ObjectIDFieldName, LeftFieldName, RightFieldName, Values, Identity, Condition, ObjectIDTableName));
            return baseQuery;
        }

        /// <summary>
        /// Adds Category Condition (with custom Binding table) to the object query.  If no categories given or none found of the given Values, will not apply a true condition (1=1).
        /// </summary>
        /// <example>If you are retrieving Foo objects that are related to categories stored in a custom binding table (Demo.FooCategory)</example>
        /// <see cref="https://github.com/KenticoDevTrev/RelationshipsExtended/tree/master/Demo/Demo/Classes/ObjectCategory"/>
        /// <param name="BindingClass">The Binding Class Code Name (ex: Demo.FooCategory)</param>
        /// <param name="ObjectIDFieldName">The Field Name of this object that matches the binding table's Left Field value. Ex: FooID</param>
        /// <param name="LeftFieldName">The Field Name of the binding class that contains this Object IDs value.  Ex: FooCategoryFooID (from Demo.FooCategory)</param>
        /// <param name="RightFieldName">The Field Name of the binding class that contains the Category's identity value.  Ex: FooCategoryCategoryID (from Demo.FooCategory) </param>
        /// <param name="Values">list of category values (int IDs, GUIDs, or string CodeNames)</param>
        /// <param name="Identity">RelEnums.IdentityType of what value is stored in the binding table for the category, default is ID</param>
        /// <param name="Condition">RelEnums.ConditionType of what type of condition to generate.</param>
        /// <param name="ObjectIDTableName">The Table Name/Alias where the ObjectIDFieldName belongs. Only needed for the 'All' Condition and if the ObjectIDField and LeftFieldName are the same. Ex: Demo_FooCategory</param>
        public static MultiObjectQuery BindingCategoryCondition(this MultiObjectQuery baseQuery, string BindingClass, string ObjectIDFieldName, string LeftFieldName, string RightFieldName, IEnumerable<object> Values, IdentityType Identity = IdentityType.ID, ConditionType Condition = ConditionType.Any, string ObjectIDTableName = null)
        {
            baseQuery.Where(RelHelper.GetBindingCategoryWhere(BindingClass, ObjectIDFieldName, LeftFieldName, RightFieldName, Values, Identity, Condition, ObjectIDTableName));
            return baseQuery;
        }

        #endregion

        #region "Custom Binding Filter"

        /// <summary>
        /// Adds Binding Condition to the document query.  If no values given or none found of the given values, will not apply a true condition (1=1).
        /// </summary>
        /// <example>You want to find Nodes that have some of the given Baz values in their relationship table Demo_NodeBaz</example>
        /// <see cref="https://github.com/KenticoDevTrev/RelationshipsExtended/tree/master/Demo/Demo/Classes/NodeObject"/>
        /// <param name="BindingClass">The Binding Class Code Name.  Ex: Demo.NodeBaz</param>
        /// <param name="ObjectClass">The Object Class Code Name (the thing that is bound to the current object through the binding table).  Ex: Demo.Baz</param>
        /// <param name="ObjectIDFieldName">The Field Name of this object that matches the binding table's Left Field value. Ex: NodeID, DocumentID</param>
        /// <param name="LeftFieldName">The Field Name of the binding class that contains this Object IDs value. Ex: NodeBazNodeID (from Demo.NodeBaz)</param>
        /// <param name="RightFieldName">The Field Name of the binding class that contains the related objects's identity value.  Ex: NodeBazBazID (from Demo.NodeBaz)</param>
        /// <param name="Values">list of object values (int IDs, GUIDs, or string CodeNames)</param>
        /// <param name="Identity">RelEnums.IdentityType of what value is stored in the binding table for the category, default is ID</param>
        /// <param name="Condition">RelEnums.ConditionType of what type of condition to generate.</param>
        /// <param name="ObjectIDTableName">The Table Name/Alias where the ObjectIDFieldName belongs. Only needed for the 'All' Condition and if the ObjectIDField and LeftFieldName are the same. Ex: Demo_NodeBaz</param>
        public static DocumentQuery BindingCondition(this DocumentQuery baseQuery, string BindingClass, string ObjectClass, string ObjectIDFieldName, string LeftFieldName, string RightFieldName, IEnumerable<string> Values, IdentityType Identity = IdentityType.ID, ConditionType Condition = ConditionType.Any, string ObjectIDTableName = null)
        {
            baseQuery.Where(RelHelper.GetBindingWhere(BindingClass, ObjectClass, ObjectIDFieldName, LeftFieldName, RightFieldName, Values, Identity, Condition, ObjectIDTableName));
            return baseQuery;
        }

        /// <summary>
        /// Adds Binding Condition to the document query.  If no values given or none found of the given values, will not apply a true condition (1=1).
        /// </summary>
        /// <example>You want to find Nodes that have some of the given Baz values in their relationship table Demo_NodeBaz</example>
        /// <see cref="https://github.com/KenticoDevTrev/RelationshipsExtended/tree/master/Demo/Demo/Classes/NodeObject"/>
        /// <param name="BindingClass">The Binding Class Code Name.  Ex: Demo.NodeBaz</param>
        /// <param name="ObjectClass">The Object Class Code Name (the thing that is bound to the current object through the binding table).  Ex: Demo.Baz</param>
        /// <param name="ObjectIDFieldName">The Field Name of this object that matches the binding table's Left Field value. Ex: NodeID, DocumentID</param>
        /// <param name="LeftFieldName">The Field Name of the binding class that contains this Object IDs value. Ex: NodeBazNodeID (from Demo.NodeBaz)</param>
        /// <param name="RightFieldName">The Field Name of the binding class that contains the related objects's identity value.  Ex: NodeBazBazID (from Demo.NodeBaz)</param>
        /// <param name="Values">list of object values (int IDs, GUIDs, or string CodeNames)</param>
        /// <param name="Identity">RelEnums.IdentityType of what value is stored in the binding table for the category, default is ID</param>
        /// <param name="Condition">RelEnums.ConditionType of what type of condition to generate.</param>
        /// <param name="ObjectIDTableName">The Table Name/Alias where the ObjectIDFieldName belongs. Only needed for the 'All' Condition and if the ObjectIDField and LeftFieldName are the same. Ex: Demo_NodeBaz</param>
        public static DocumentQuery<TDocument> BindingCondition<TDocument>(DocumentQuery<TDocument> baseQuery, string BindingClass, string ObjectClass, string ObjectIDFieldName, string LeftFieldName, string RightFieldName, IEnumerable<string> Values, IdentityType Identity = IdentityType.ID, ConditionType Condition = ConditionType.Any, string ObjectIDTableName = null) where TDocument : TreeNode, new()
        {
            baseQuery.Where(RelHelper.GetBindingWhere(BindingClass, ObjectClass, ObjectIDFieldName, LeftFieldName, RightFieldName, Values, Identity, Condition, ObjectIDTableName));
            return baseQuery;
        }

        /// <summary>
        /// Adds Binding Condition to the document query.  If no values given or none found of the given values, will not apply a true condition (1=1).
        /// </summary>
        /// <example>You want to find Nodes that have some of the given Baz values in their relationship table Demo_NodeBaz</example>
        /// <see cref="https://github.com/KenticoDevTrev/RelationshipsExtended/tree/master/Demo/Demo/Classes/NodeObject"/>
        /// <param name="BindingClass">The Binding Class Code Name.  Ex: Demo.NodeBaz</param>
        /// <param name="ObjectClass">The Object Class Code Name (the thing that is bound to the current object through the binding table).  Ex: Demo.Baz</param>
        /// <param name="ObjectIDFieldName">The Field Name of this object that matches the binding table's Left Field value. Ex: NodeID, DocumentID</param>
        /// <param name="LeftFieldName">The Field Name of the binding class that contains this Object IDs value. Ex: NodeBazNodeID (from Demo.NodeBaz)</param>
        /// <param name="RightFieldName">The Field Name of the binding class that contains the related objects's identity value.  Ex: NodeBazBazID (from Demo.NodeBaz)</param>
        /// <param name="Values">list of object values (int IDs, GUIDs, or string CodeNames)</param>
        /// <param name="Identity">RelEnums.IdentityType of what value is stored in the binding table for the category, default is ID</param>
        /// <param name="Condition">RelEnums.ConditionType of what type of condition to generate.</param>
        /// <param name="ObjectIDTableName">The Table Name/Alias where the ObjectIDFieldName belongs. Only needed for the 'All' Condition and if the ObjectIDField and LeftFieldName are the same. Ex: Demo_NodeBaz</param>
        public static MultiDocumentQuery BindingCondition(this MultiDocumentQuery baseQuery, string BindingClass, string ObjectClass, string ObjectIDFieldName, string LeftFieldName, string RightFieldName, IEnumerable<string> Values, IdentityType Identity = IdentityType.ID, ConditionType Condition = ConditionType.Any, string ObjectIDTableName = null)
        {
            baseQuery.Where(RelHelper.GetBindingWhere(BindingClass, ObjectClass, ObjectIDFieldName, LeftFieldName, RightFieldName, Values, Identity, Condition, ObjectIDTableName));
            return baseQuery;
        }

        /// <summary>
        /// Adds Binding Condition to the object query.  If no values given or none found of the given values, will not apply a true condition (1=1).
        /// </summary>
        /// <example>You want to find Foos that have some of the given Baz values in their relationship table Demo_FooBaz</example>
        /// <see cref="https://github.com/KenticoDevTrev/RelationshipsExtended/tree/master/Demo/Demo/Classes/ObjectObject"/>
        /// <param name="BindingClass">The Binding Class Code Name.  Ex: Demo.FooBaz</param>
        /// <param name="ObjectClass">The Object Class Code Name (the thing that is bound to the current object through the binding table).  Ex: Demo.Baz</param>
        /// <param name="ObjectIDFieldName">The Field Name of this object that matches the binding table's Left Field value. Ex: FooID</param>
        /// <param name="LeftFieldName">The Field Name of the binding class that contains this Object IDs value. Ex: FooBazFooID (from Demo.NodeBaz)</param>
        /// <param name="RightFieldName">The Field Name of the binding class that contains the related objects's identity value.  Ex: FooBazBazID (from Demo.NodeBaz)</param>
        /// <param name="Values">list of object values (int IDs, GUIDs, or string CodeNames) (ex 'BazA', 'BazB')</param>
        /// <param name="Identity">RelEnums.IdentityType of what value is stored in the binding table for the category, default is ID</param>
        /// <param name="Condition">RelEnums.ConditionType of what type of condition to generate.</param>
        /// <param name="ObjectIDTableName">The Table Name/Alias where the ObjectIDFieldName belongs. Only needed for the 'All' Condition and if the ObjectIDField and LeftFieldName are the same. Ex: Demo_FooBaz</param>
        public static ObjectQuery BindingCondition(this ObjectQuery baseQuery, string BindingClass, string ObjectClass, string ObjectIDFieldName, string LeftFieldName, string RightFieldName, IEnumerable<string> Values, IdentityType Identity = IdentityType.ID, ConditionType Condition = ConditionType.Any, string ObjectIDTableName = null)
        {
            baseQuery.Where(RelHelper.GetBindingWhere(BindingClass, ObjectClass, ObjectIDFieldName, LeftFieldName, RightFieldName, Values, Identity, Condition, ObjectIDTableName));
            return baseQuery;
        }

        /// <summary>
        /// Adds Binding Condition to the object query.  If no values given or none found of the given values, will not apply a true condition (1=1).
        /// </summary>
        /// <example>You want to find Foos that have some of the given Baz values in their relationship table Demo_FooBaz</example>
        /// <see cref="https://github.com/KenticoDevTrev/RelationshipsExtended/tree/master/Demo/Demo/Classes/ObjectObject"/>
        /// <param name="BindingClass">The Binding Class Code Name.  Ex: Demo.FooBaz</param>
        /// <param name="ObjectClass">The Object Class Code Name (the thing that is bound to the current object through the binding table).  Ex: Demo.Baz</param>
        /// <param name="ObjectIDFieldName">The Field Name of this object that matches the binding table's Left Field value. Ex: FooID</param>
        /// <param name="LeftFieldName">The Field Name of the binding class that contains this Object IDs value. Ex: FooBazFooID (from Demo.NodeBaz)</param>
        /// <param name="RightFieldName">The Field Name of the binding class that contains the related objects's identity value.  Ex: FooBazBazID (from Demo.NodeBaz)</param>
        /// <param name="Values">list of object values (int IDs, GUIDs, or string CodeNames) (ex 'BazA', 'BazB')</param>
        /// <param name="Identity">RelEnums.IdentityType of what value is stored in the binding table for the category, default is ID</param>
        /// <param name="Condition">RelEnums.ConditionType of what type of condition to generate.</param>
        /// <param name="ObjectIDTableName">The Table Name/Alias where the ObjectIDFieldName belongs. Only needed for the 'All' Condition and if the ObjectIDField and LeftFieldName are the same. Ex: Demo_FooBaz</param>
        public static ObjectQuery<TObject> BindingCondition<TObject>(ObjectQuery<TObject> baseQuery, string BindingClass, string ObjectClass, string ObjectIDFieldName, string LeftFieldName, string RightFieldName, IEnumerable<string> Values, IdentityType Identity = IdentityType.ID, ConditionType Condition = ConditionType.Any, string ObjectIDTableName = null) where TObject : BaseInfo, new()
        {
            baseQuery.Where(RelHelper.GetBindingWhere(BindingClass, ObjectClass, ObjectIDFieldName, LeftFieldName, RightFieldName, Values, Identity, Condition, ObjectIDTableName));
            return baseQuery;
        }

        /// <summary>
        /// Adds Binding Condition to the object query.  If no values given or none found of the given values, will not apply a true condition (1=1).
        /// </summary>
        /// <example>You want to find Foos that have some of the given Baz values in their relationship table Demo_FooBaz</example>
        /// <see cref="https://github.com/KenticoDevTrev/RelationshipsExtended/tree/master/Demo/Demo/Classes/ObjectObject"/>
        /// <param name="BindingClass">The Binding Class Code Name.  Ex: Demo.FooBaz</param>
        /// <param name="ObjectClass">The Object Class Code Name (the thing that is bound to the current object through the binding table).  Ex: Demo.Baz</param>
        /// <param name="ObjectIDFieldName">The Field Name of this object that matches the binding table's Left Field value. Ex: FooID</param>
        /// <param name="LeftFieldName">The Field Name of the binding class that contains this Object IDs value. Ex: FooBazFooID (from Demo.NodeBaz)</param>
        /// <param name="RightFieldName">The Field Name of the binding class that contains the related objects's identity value.  Ex: FooBazBazID (from Demo.NodeBaz)</param>
        /// <param name="Values">list of object values (int IDs, GUIDs, or string CodeNames) (ex 'BazA', 'BazB')</param>
        /// <param name="Identity">RelEnums.IdentityType of what value is stored in the binding table for the category, default is ID</param>
        /// <param name="Condition">RelEnums.ConditionType of what type of condition to generate.</param>
        /// <param name="ObjectIDTableName">The Table Name/Alias where the ObjectIDFieldName belongs. Only needed for the 'All' Condition and if the ObjectIDField and LeftFieldName are the same. Ex: Demo_FooBaz</param>
        public static MultiObjectQuery BindingCondition(this MultiObjectQuery baseQuery, string BindingClass, string ObjectClass, string ObjectIDFieldName, string LeftFieldName, string RightFieldName, IEnumerable<string> Values, IdentityType Identity = IdentityType.ID, ConditionType Condition = ConditionType.Any, string ObjectIDTableName = null)
        {
            baseQuery.Where(RelHelper.GetBindingWhere(BindingClass, ObjectClass, ObjectIDFieldName, LeftFieldName, RightFieldName, Values, Identity, Condition, ObjectIDTableName));
            return baseQuery;
        }

        #endregion

        private static DocumentQuery InRelationWithOrderInternal(DocumentQuery baseQuery, int nodeID, int? RelationshipNameID, bool Ordered = true, bool AscendingOrder = true, bool ReverseRelationship = false)
        {
            // Add the Inner Join with proper alias formatting
            string RelatedColumn = (ReverseRelationship ? "LeftNodeID" : "RightNodeID");
            string MatchColumn = (ReverseRelationship ? "RightNodeID" : "LeftNodeID");
            if (RelationshipNameID.HasValue)
            {
                baseQuery.Source((QuerySource s) => s.InnerJoin(new QuerySourceTable("CMS_Relationship"), new WhereCondition($"NodeID = {RelatedColumn}").WhereEquals("RelationshipNameID", RelationshipNameID.Value).WhereEquals(MatchColumn, nodeID)));
            }
            else
            {
                baseQuery.Source((QuerySource s) => s.InnerJoin(new QuerySourceTable("CMS_Relationship"), new WhereCondition($"NodeID = {RelatedColumn}").WhereEquals(MatchColumn, nodeID)));
            }

            if (!ReverseRelationship && Ordered)
            {
                // add the by the Relationship Order
                if (AscendingOrder)
                {
                    baseQuery.OrderBy("RelationshipOrder");
                }
                else
                {
                    baseQuery.OrderByDescending("RelationshipOrder");
                }
            }

            return baseQuery;
        }

        private static DocumentQuery<TDocument> InRelationWithOrderInternal<TDocument>(DocumentQuery<TDocument> baseQuery, int nodeID, int? RelationshipNameID, bool Ordered = true, bool AscendingOrder = true, bool ReverseRelationship = false) where TDocument : TreeNode, new()
        {
            // Add the Inner Join with proper alias formatting
            string RelatedColumn = (ReverseRelationship ? "LeftNodeID" : "RightNodeID");
            string MatchColumn = (ReverseRelationship ? "RightNodeID" : "LeftNodeID");
            if (RelationshipNameID.HasValue)
            {
                baseQuery.Source((QuerySource s) => s.InnerJoin(new QuerySourceTable("CMS_Relationship"), new WhereCondition($"NodeID = {RelatedColumn}").WhereEquals("RelationshipNameID", RelationshipNameID.Value).WhereEquals(MatchColumn, nodeID)));
            }
            else
            {
                baseQuery.Source((QuerySource s) => s.InnerJoin(new QuerySourceTable("CMS_Relationship"), new WhereCondition($"NodeID = {RelatedColumn}").WhereEquals(MatchColumn, nodeID)));
            }

            if (!ReverseRelationship && Ordered)
            {
                // add the by the Relationship Order
                if (AscendingOrder)
                {
                    baseQuery.OrderBy("RelationshipOrder");
                }
                else
                {
                    baseQuery.OrderByDescending("RelationshipOrder");
                }
            }

            return baseQuery;
        }

        private static MultiDocumentQuery InRelationWithOrderInternal(MultiDocumentQuery baseQuery, int nodeID, int? RelationshipNameID, bool Ordered = true, bool AscendingOrder = true, bool ReverseRelationship = false)
        {
            // Add the Inner Join with proper alias formatting
            string RelatedColumn = (ReverseRelationship ? "LeftNodeID" : "RightNodeID");
            string MatchColumn = (ReverseRelationship ? "RightNodeID" : "LeftNodeID");
            if (RelationshipNameID.HasValue)
            {
                baseQuery.Source((QuerySource s) => s.InnerJoin(new QuerySourceTable("CMS_Relationship"), new WhereCondition($"NodeID = {RelatedColumn}").WhereEquals("RelationshipNameID", RelationshipNameID.Value).WhereEquals(MatchColumn, nodeID)));
            }
            else
            {
                baseQuery.Source((QuerySource s) => s.InnerJoin(new QuerySourceTable("CMS_Relationship"), new WhereCondition($"NodeID = {RelatedColumn}").WhereEquals(MatchColumn, nodeID)));
            }

            if (!ReverseRelationship && Ordered)
            {
                // add the by the Relationship Order
                if (AscendingOrder)
                {
                    baseQuery.OrderBy("RelationshipOrder");
                }
                else
                {
                    baseQuery.OrderByDescending("RelationshipOrder");
                }
            }

            return baseQuery;
        }


        

        /// <summary>
        /// Gets the Lookup value for the class, converting whatever the value is passed to the ID type.
        /// </summary>
        /// <param name="PrimaryClass"></param>
        /// <param name="InRelationshipWithValue"></param>
        /// <param name="Identity"></param>
        /// <returns></returns>
        private static object GetLookupValue(string PrimaryClass, object InRelationshipWithValue, IdentityType Identity)
        {
            switch (Identity)
            {
                default:
                case IdentityType.ID:
                    return RelHelper.ObjectIdentitiesToIDs(RelHelper.GetClassObjSummary(PrimaryClass), new object[] { InRelationshipWithValue }).FirstOrDefault();
                case IdentityType.CodeName:
                    return RelHelper.ObjectIdentitiesToCodeNames(RelHelper.GetClassObjSummary(PrimaryClass), new object[] { InRelationshipWithValue }).FirstOrDefault();
                case IdentityType.Guid:
                    return RelHelper.ObjectIdentitiesToGUIDs(RelHelper.GetClassObjSummary(PrimaryClass), new object[] { InRelationshipWithValue }).FirstOrDefault();
            }
        }

        /// <summary>
        /// Gets the NodeID from NodeGuid
        /// </summary>
        /// <param name="nodeGuid"></param>
        /// <returns>The NodeID</returns>
        private static int? GetNodeID(Guid nodeGuid)
        {
            return CacheHelper.Cache(cs =>
            {
                TreeNode node = DocumentHelper.GetDocuments()
                .WhereEquals("NodeGuid", nodeGuid)
                .CombineWithAnyCulture()
                .CombineWithDefaultCulture()
                .Columns("NodeID")
                .FirstOrDefault();

                if (node != null)
                {
                    if (cs.Cached)
                    {
                        cs.CacheDependency = CacheHelper.GetCacheDependency($"nodeid|{node.NodeID}");
                    }
                }

                return node?.NodeID;
            }, new CacheSettings(1440, "RelExtendedGetNodeID", nodeGuid));
        }

        private static int? GetRelationshipNameID(string RelationshipName)
        {
            return CacheHelper.Cache(cs =>
            {
                if (cs.Cached)
                {
                    cs.CacheDependency = CacheHelper.GetCacheDependency($"cms.relationshipname|byname|{RelationshipName}");
                }
                var Relationship = RelationshipNameInfo.Provider.Get(RelationshipName);
                return Relationship?.RelationshipNameId;
            }, new CacheSettings(1440, "RelExtendedGetRelationshipNameID", RelationshipName));
        }
    }
}
