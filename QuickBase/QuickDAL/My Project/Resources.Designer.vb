﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:2.0.50727.1433
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On

Imports System

Namespace My.Resources
    
    'This class was auto-generated by the StronglyTypedResourceBuilder
    'class via a tool like ResGen or Visual Studio.
    'To add or remove a member, edit your .ResX file then rerun ResGen
    'with the /str option, or rebuild your VS project.
    '''<summary>
    '''  A strongly-typed resource class, for looking up localized strings, etc.
    '''</summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0"),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     Global.Microsoft.VisualBasic.HideModuleNameAttribute()>  _
    Friend Module Resources
        
        Private resourceMan As Global.System.Resources.ResourceManager
        
        Private resourceCulture As Global.System.Globalization.CultureInfo
        
        '''<summary>
        '''  Returns the cached ResourceManager instance used by this class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("QuickDAL.Resources", GetType(Resources).Assembly)
                    resourceMan = temp
                End If
                Return resourceMan
            End Get
        End Property
        
        '''<summary>
        '''  Overrides the current thread's CurrentUICulture property for all
        '''  resource lookups using this strongly typed resource class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set
                resourceCulture = value
            End Set
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to COA ID must be greater than 0..
        '''</summary>
        Friend ReadOnly Property RuleDescCOAIDGreaterThanZero() As String
            Get
                Return ResourceManager.GetString("RuleDescCOAIDGreaterThanZero", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Company ID must be greater than 0..
        '''</summary>
        Friend ReadOnly Property RuleDescCoIDGreaterThanZero() As String
            Get
                Return ResourceManager.GetString("RuleDescCoIDGreaterThanZero", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Entity Type is not provided, it is required to save the record..
        '''</summary>
        Friend ReadOnly Property RuleDescEntityTypeRequired() As String
            Get
                Return ResourceManager.GetString("RuleDescEntityTypeRequired", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Opening Debit and Credit both can not be greater than 0..
        '''</summary>
        Friend ReadOnly Property RuleDescOpeningDrAndCrMutuallyExlusive() As String
            Get
                Return ResourceManager.GetString("RuleDescOpeningDrAndCrMutuallyExlusive", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Party Code is not provided, it is required to save the record..
        '''</summary>
        Friend ReadOnly Property RuleDescPartyCodeRequired() As String
            Get
                Return ResourceManager.GetString("RuleDescPartyCodeRequired", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Party ID must be greater than 0 [Contact vender if party id is auto generated]..
        '''</summary>
        Friend ReadOnly Property RuleDescPartyIDGreaterThanZero() As String
            Get
                Return ResourceManager.GetString("RuleDescPartyIDGreaterThanZero", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Party Name is not provided, it is required to save the record..
        '''</summary>
        Friend ReadOnly Property RuleDescPartyNameRequired() As String
            Get
                Return ResourceManager.GetString("RuleDescPartyNameRequired", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Invalid COA ID.
        '''</summary>
        Friend ReadOnly Property RuleNameCOAIDGreaterThanZero() As String
            Get
                Return ResourceManager.GetString("RuleNameCOAIDGreaterThanZero", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Invalid Company ID.
        '''</summary>
        Friend ReadOnly Property RuleNameCoIDGreaterThanZero() As String
            Get
                Return ResourceManager.GetString("RuleNameCoIDGreaterThanZero", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Entity Type Required.
        '''</summary>
        Friend ReadOnly Property RuleNameEntityTypeRequired() As String
            Get
                Return ResourceManager.GetString("RuleNameEntityTypeRequired", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Opening Debit and Credit Mutually Exlusive.
        '''</summary>
        Friend ReadOnly Property RuleNameOpeningDrAndCrMutuallyExlusive() As String
            Get
                Return ResourceManager.GetString("RuleNameOpeningDrAndCrMutuallyExlusive", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Party Code Required.
        '''</summary>
        Friend ReadOnly Property RuleNamePartyCodeRequired() As String
            Get
                Return ResourceManager.GetString("RuleNamePartyCodeRequired", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Invalid Party ID.
        '''</summary>
        Friend ReadOnly Property RuleNamePartyIDGreaterThanZero() As String
            Get
                Return ResourceManager.GetString("RuleNamePartyIDGreaterThanZero", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Party Name Required.
        '''</summary>
        Friend ReadOnly Property RuleNamePartyNameRequired() As String
            Get
                Return ResourceManager.GetString("RuleNamePartyNameRequired", resourceCulture)
            End Get
        End Property
    End Module
End Namespace