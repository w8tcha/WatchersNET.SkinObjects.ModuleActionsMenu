#region Copyright

//
// DotNetNuke® - http://www.dotnetnuke.com
// Copyright (c) 2002-2011
// by DotNetNuke Corporation
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
// the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
// to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions 
// of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.

#endregion

namespace WatchersNET.SkinObjects.ModuleActionsMenu
{
    #region

    using System;
    using System.Web.UI;

    using DotNetNuke.Entities.Modules.Actions;
    using DotNetNuke.Entities.Portals;
    using DotNetNuke.Services.Exceptions;
    using DotNetNuke.Services.Localization;
    using DotNetNuke.UI.Containers;
    using DotNetNuke.UI.Modules;
    using DotNetNuke.UI.WebControls;

    #endregion

    /// -----------------------------------------------------------------------------
    /// Project : DotNetNuke
    /// Namespace: DotNetNuke.UI.Containers
    /// Class : ActionBase
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// ActionBase is an abstract base control for Action objects that inherit from UserControl.
    /// </summary>
    /// <remarks>
    /// ActionBase inherits from UserControl, and implements the IActionControl Interface
    /// </remarks>
    /// <history>
    ///   [cnurse]   10/07/2004   Documented
    ///   [cnurse]    12/15/2007  Refactored 
    /// </history>
    /// -----------------------------------------------------------------------------
    public abstract class ModuleActionsBase : UserControl, IActionControl
    {
        #region Constants and Fields

        /// <summary>
        /// The m_supports icons.
        /// </summary>
        protected bool _supportsIcons = true;

        /// <summary>
        /// The _ action manager.
        /// </summary>
        private ActionManager _actionManager;

        /// <summary>
        /// The _ action root.
        /// </summary>
        private ModuleAction _actionRoot;

        #endregion

        #region Events

        /// <summary>
        /// The action.
        /// </summary>
        public event ActionEventHandler Action;

        #endregion

        #region Properties

        /// <summary>
        ///   Gets ActionManager.
        /// </summary>
        public ActionManager ActionManager
        {
            get
            {
                return this._actionManager ?? (this._actionManager = new ActionManager(this));
            }
        }

        /// <summary>
        ///   Gets a value indicating whether EditMode.
        /// </summary>
        public bool EditMode
        {
            get
            {
                return this.ModuleContext.PortalSettings.UserMode != PortalSettings.Mode.View;
            }
        }

        /// <summary>
        ///   Gets or sets ModuleControl.
        /// </summary>
        public IModuleControl ModuleControl { get; set; }

        /// <summary>
        ///   Gets a value indicating whether SupportsIcons.
        /// </summary>
        public bool SupportsIcons
        {
            get
            {
                return this._supportsIcons;
            }
        }

        /// <summary>
        ///   Gets ActionRoot.
        /// </summary>
        protected ModuleAction ActionRoot
        {
            get
            {
                return this._actionRoot
                       ??
                       (this._actionRoot =
                        new ModuleAction(
                            this.ModuleContext.GetNextActionID(), 
                            Localization.GetString("Manage.Text", Localization.GlobalResourceFile), 
                            string.Empty, 
                            string.Empty, 
                            "manage-icn.png"));
            }
        }

        /// <summary>
        ///   Gets the Actions Collection
        /// </summary>
        protected ModuleActionCollection Actions
        {
            get
            {
                return this.ModuleContext.Actions;
            }
        }

        /// <summary>
        ///   Gets ModuleContext.
        /// </summary>
        protected ModuleInstanceContext ModuleContext
        {
            get
            {
                return this.ModuleControl.ModuleContext;
            }
        }

        /// <summary>
        /// Gets PortalSettings.
        /// </summary>
        protected PortalSettings PortalSettings
        {
            get
            {
                return this.ModuleControl.ModuleContext.PortalSettings;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// DisplayControl determines whether the control should be displayed
        /// </summary>
        /// <param name="objNodes">
        /// The obj nodes.
        /// </param>
        /// <returns>
        /// The display control.
        /// </returns>
        protected bool DisplayControl(DNNNodeCollection objNodes)
        {
            return this.ActionManager.DisplayControl(objNodes);
        }

        /// <summary>
        /// OnAction raises the Action Event for this control
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        protected virtual void OnAction(ActionEventArgs e)
        {
            if (this.Action != null)
            {
                this.Action(this, e);
            }
        }

        /// <summary>
        /// The on load.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            try
            {
                this.ActionRoot.Actions.AddRange(this.Actions);
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        /// <summary>
        /// ProcessAction processes the action event
        /// </summary>
        /// <param name="actionID">
        /// The action id.
        /// </param>
        protected void ProcessAction(string actionID)
        {
            int output;

            if (!int.TryParse(actionID, out output))
            {
                return;
            }

            ModuleAction action = this.Actions.GetActionByID(output);

            if (action == null)
            {
                return;
            }

            if (!this.ActionManager.ProcessAction(action))
            {
                this.OnAction(new ActionEventArgs(action, this.ModuleContext.Configuration));
            }
        }

        #endregion
    }
}