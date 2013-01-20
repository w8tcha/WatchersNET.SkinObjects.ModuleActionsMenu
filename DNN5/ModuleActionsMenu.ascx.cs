/*  *********************************************************************************************
*
*   WatchersNET.SkinObjects.ModuleActionsMenu (MaM)
*   http://dnnmoduleactions.codeplex.com/
*   The Module ActionsMenu Renders the Module Action List of an Module as an a simple HTML 
*   unordered list instead of Heavy loaded Java Script lists.
*
*   Copyright(c) Ingo Herbote
*   All rights reserved.
*   Ingo Herbote (thewatcher@watchersnet.de)
*   Internet: http://www.watchersnet.de
*
*   WatchersNET ModuleActionsMenu is released under the New BSD License, see below
************************************************************************************************
*
*   Redistribution and use in source and binary forms, with or without modification, 
*   are permitted provided that the following conditions are met:
*
*   * Redistributions of source code must retain the above copyright notice,
*   this list of conditions and the following disclaimer.
*
*   * Redistributions in binary form must reproduce the above copyright notice, 
*   this list of conditions and the following disclaimer in the documentation and/
*   or other materials provided with the distribution.
*
*   * Neither the name of WatchersNET nor the names of its contributors 
*   may be used to endorse or promote products derived from this software without 
*   specific prior written permission.
*
*   THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
*   ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES 
*   OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
*   IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, 
*   INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, 
*   PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
*   INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT 
*   LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE 
*   OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE. 
*
************************************************************************************************
*/

namespace WatchersNET.SkinObjects.ModuleActionsMenu
{
    #region

    using System;
    using System.Text;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    using DotNetNuke.Entities.Portals;
    using DotNetNuke.Framework;
    using DotNetNuke.Modules.NavigationProvider;
    using DotNetNuke.Services.Exceptions;
    using DotNetNuke.UI;
    using DotNetNuke.UI.Containers;
    using DotNetNuke.UI.WebControls;

    #endregion

    /// <summary>
    /// The menu.
    /// </summary>
    public partial class Menu : ActionBase
    {
        #region Constants and Fields

        /// <summary>
        /// The user agent.
        /// </summary>
        private string userAgent;

        /// <summary>
        /// The module menu control.
        /// </summary>
        private HtmlGenericControl ctlModulMenu;

        /// <summary>
        /// The module menu link.
        /// </summary>
        private LinkButton ctlModulMenuLink;

        /// <summary>
        /// The module menu link image.
        /// </summary>
        private HtmlImage ctlModulMenuLinkImage;

        /// <summary>
        /// The module menu main UL.
        /// </summary>
        private HtmlGenericControl ctlModulMenuMainUl;

        /// <summary>
        /// The module menu UL.
        /// </summary>
        private HtmlGenericControl ctlModulMenuUl;

        /// <summary>
        /// The module menu LI.
        /// </summary>
        private HtmlGenericControl ctlModulMenuli;

        /// <summary>
        /// The b context mode.
        /// </summary>
        private bool bContextMode = true;

        /// <summary>
        /// The Context Attach Item
        /// </summary>
        private string contextAttachItemID = string.Empty;

        /// <summary>
        /// The include CSS file.
        /// </summary>
        private bool bIncludeCssFile = true;

        /// <summary>
        /// The include JS.
        /// </summary>
        private bool bIncludeJs = true;

        /// <summary>
        /// The Display Icon.
        /// </summary>
        private bool displayIcon = true;

        /// <summary>
        /// The Display Link.
        /// </summary>
        private bool displayLink = true;

        /// <summary>
        /// The start expand depth.
        /// </summary>
        private int objIntExpandDepth = -1;

        /// <summary>
        /// The Skin Name.
        /// </summary>
        private string sCssFile = "Grey";

        /// <summary>
        /// The Main Menu Icon.
        /// </summary>
        private string sMenuIcon = "~/DesktopModules/WatchersNET.SkinObjects.ModuleActionsMenu/skins/action.png";

        /// <summary>
        /// The Main Menu Link Text.
        /// </summary>
        private string sMenuLink = "Manage";

        #endregion

        //// 
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Menu"/> class.
        /// </summary>
        protected Menu()
        {
            this.Load += this.PageLoad;
            this.ProviderName = "DNNDropDownNavigationProvider";
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether ContextMode.
        /// </summary>
        public bool ContextMode
        {
            get
            {
                return this.bContextMode;
            }

            set
            {
                this.bContextMode = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether show Main Menu Icon.
        /// </summary>
        public bool DisplayIcon
        {
            get
            {
                return this.displayIcon;
            }

            set
            {
                this.displayIcon = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether show Main Menu Link Text.
        /// </summary>
        public bool DisplayLink
        {
            get
            {
                return this.displayLink;
            }

            set
            {
                this.displayLink = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Context Attach Item is defined
        /// </summary>
        public string ContextAttachItemID
        {
            get
            {
                return this.contextAttachItemID;
            }

            set
            {
                this.contextAttachItemID = value;
            }
        }

        /// <summary>
        /// Gets Control.
        /// </summary>
        public NavigationProvider Control { get; private set; }

        /// <summary>
        /// Gets or sets The Main Menu Menu Icon.
        /// </summary>
        public string MenuIcon
        {
            get
            {
                return this.sMenuIcon;
            }

            set
            {
                this.sMenuIcon = value;
            }
        }

        /// <summary>
        /// Gets or sets The Main Menu Link Text
        /// </summary>
        public string MenuLink
        {
            get
            {
                return this.sMenuLink;
            }

            set
            {
                this.sMenuLink = value;
            }
        }

        /// <summary>
        /// Gets or sets The CSS File.
        /// </summary>
        public string CssFile
        {
            get
            {
                return this.sCssFile;
            }

            set
            {
                this.sCssFile = value;
            }
        }

        /// <summary>
        /// Gets or sets ExpandDepth.
        /// </summary>
        public int ExpandDepth
        {
            get
            {
                if (!this.PopulateNodesFromClient)
                {
                    return -1;
                }

                return this.objIntExpandDepth;
            }

            set
            {
                this.objIntExpandDepth = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Include the CSS File.
        /// </summary>
        public bool IncludeCssFile
        {
            get
            {
                return this.bIncludeCssFile;
            }

            set
            {
                this.bIncludeCssFile = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Include the JS File.
        /// </summary>
        public bool IncludeJs
        {
            get
            {
                return this.bIncludeJs;
            }

            set
            {
                this.bIncludeJs = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether PopulateNodesFromClient.
        /// </summary>
        public bool PopulateNodesFromClient { get; set; }

        /// <summary>
        /// Gets or sets ProviderName.
        /// </summary>
        public string ProviderName { get; set; }

        /// <summary>
        /// Gets ModulePath.
        /// </summary>
        private string ModulePath
        {
            get
            {
                return ModuleControl.ModuleContext.Configuration.ContainerPath.Substring(
                    0, ModuleControl.ModuleContext.Configuration.ContainerPath.LastIndexOf("/", StringComparison.Ordinal) + 1);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.Control = NavigationProvider.Instance(this.ProviderName);
            this.Control.ControlID = string.Format("ctl{0}", this.ID);
            this.Control.Initialize();
        }

        /// <summary>
        /// The Page Load.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void PageLoad(object sender, EventArgs e)
        {
            // Make sure its not in View Mode
            if (PortalSettings.UserMode == PortalSettings.Mode.View)
            {
                return;
            }

            this.userAgent = this.Request.UserAgent;

            double version = this.GetInternetExplorerVersion();

            if (version > 0.0)
            {
                if (version < 6.0)
                {
                    this.bContextMode = false;
                }
            }

            if (this.sCssFile.Contains("Context-"))
            {
                this.sCssFile = this.sCssFile.Replace("Context-", string.Empty);
            }
            else if (this.sCssFile.Contains("ModuleMenu-"))
            {
                this.sCssFile = this.sCssFile.Replace("ModuleMenu-", string.Empty);
            }

            string classNameNormal = !string.IsNullOrEmpty(this.sCssFile) &&
                                                                    this.IncludeCssFile
                                                                        ? string.Format(
                                                                            "ModuleOptionsMenu-{0}", this.sCssFile)
                                                                        : "ModuleOptionsMenu";

            string classNameContext = !string.IsNullOrEmpty(this.sCssFile) &&
                                                                    this.IncludeCssFile
                                                                        ? string.Format(
                                                                            "ModuleContextMenu-{0}", this.sCssFile)
                                                                        : "ModuleContextMenu";

            this.LoadMenu(classNameNormal, classNameContext);
        }

        /// <summary>
        /// Returns the version of Internet Explorer or a -1
        ///   (indicating the use of another browser).
        /// </summary>
        /// <returns>
        /// The get internet explorer version.
        /// </returns>
        private float GetInternetExplorerVersion()
        {
            float rv = -1;

            HttpBrowserCapabilities browser = this.Request.Browser;

            if (browser.Browser.Equals("IE"))
            {
                rv = (float)(browser.MajorVersion + browser.MinorVersion);
            }

            return rv;
        }

        /// <summary>
        /// Include StyleSheet (Skin)
        /// </summary>
        private void IncludeStyleSheet()
        {
            if (string.IsNullOrEmpty(this.sCssFile) || !this.bIncludeCssFile)
            {
                return;
            }

            if (this.sCssFile.Contains("Context-"))
            {
                this.sCssFile = this.sCssFile.Replace("Context-", string.Empty);
            }
            else if (this.sCssFile.Contains("ModuleMenu-"))
            {
                this.sCssFile = this.sCssFile.Replace("ModuleMenu-", string.Empty);
            }

            ((CDefault)this.Page).AddStyleSheet(
                this.sCssFile, this.ResolveUrl(string.Format("skins/{0}.css", this.sCssFile)), true);
        }

        /// <summary>
        /// Loads the Menu.
        /// </summary>
        /// <param name="cssClassNameNormal">
        /// The CSS Class Name Normal.
        /// </param>
        /// <param name="cssClassNameContext">
        /// The CSS Class Name Context.
        /// </param>
        private void LoadMenu(string cssClassNameNormal, string cssClassNameContext)
        {
            try
            {
                // DNN 5.x
                DNNNodeCollection objNodes = Navigation.GetActionNodes(this.ActionRoot, this, -1);

                this.RegisterJavaScript(cssClassNameNormal, cssClassNameContext);

                this.IncludeStyleSheet();

                foreach (DNNNode node in objNodes)
                {
                    if (node.DNNNodes.Count <= 1)
                    {
                        continue;
                    }

                    // ContextMenu
                    if (this.bContextMode && this.IncludeJs)
                    {
                        this.ctlModulMenuUl = new HtmlGenericControl("ul");

                        this.ctlModulMenuUl.Attributes["id"] = this.ClientID;

                        this.ctlModulMenuUl.Attributes["class"] = cssClassNameContext;

                        this.phModuleMenu.Controls.Add(this.ctlModulMenuUl);
                    }
                    else
                    {
                        this.ctlModulMenuMainUl = new HtmlGenericControl("ul");
                        this.phModuleMenu.Controls.Add(this.ctlModulMenuMainUl);

                        this.ctlModulMenuMainUl.Attributes["class"] = cssClassNameNormal;

                        this.ctlModulMenuli = new HtmlGenericControl("li");
                        this.ctlModulMenuMainUl.Controls.Add(this.ctlModulMenuli);

                        // Render Main Link Text
                        if (!string.IsNullOrEmpty(this.sMenuLink) && this.displayLink)
                        {
                            this.ctlModulMenuli.InnerText = this.sMenuLink;
                        }

                        // Render Main Icon
                        if (!string.IsNullOrEmpty(this.sMenuIcon) && this.displayIcon)
                        {
                            var mainIcon = new HtmlImage
                                {
                                    Src = this.SetIconPath(this.MenuIcon, true), 
                                    Alt = "ModuleActionsMenu"
                                };

                            mainIcon.Attributes.Add("title", "Module Actions Menu");

                            this.ctlModulMenuli.Controls.Add(mainIcon);
                        }

                        string mainIconCss = cssClassNameNormal.Replace(
                            "ModuleOptionsMenu", "ModuleOptionsMenuMainIcon");

                        if (!this.displayLink)
                        {
                            mainIconCss = string.Format("{0} MainLinkText", mainIconCss);
                        }

                        this.ctlModulMenuli.Attributes["class"] = mainIconCss;

                        if (this.userAgent.IndexOf("MSIE") > -1)
                        {
                            this.ctlModulMenuli.Attributes["onmouseover"] =
                                string.Format("this.className='{0} sfhover'", mainIconCss);
                            this.ctlModulMenuli.Attributes["onmouseout"] = string.Format("this.className='{0}'", mainIconCss);
                        }

                        this.ctlModulMenuUl = new HtmlGenericControl("ul");

                        this.ctlModulMenuli.Controls.Add(this.ctlModulMenuUl);
                    }

                    foreach (DNNNode mainNode in node.DNNNodes)
                    {
                        this.RenderNode(mainNode, this.ctlModulMenuUl);
                    }
                }
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        /// <summary>
        /// Renders the current node
        /// </summary>
        /// <param name="mainNode">
        /// The main node.
        /// </param>
        /// <param name="modulMenuUl">
        /// The module Menu UL.
        /// </param>
        private void RenderNode(DNNNode mainNode, HtmlGenericControl modulMenuUl)
        {
            this.ctlModulMenu = new HtmlGenericControl("li");

            var modulSubMenuUl = new HtmlGenericControl("ul");

            modulMenuUl.Controls.Add(this.ctlModulMenu);

            if (this.userAgent.IndexOf("MSIE") > -1)
            {
                this.ctlModulMenu.Attributes["onmouseover"] = "this.className+=' sfhover'";
                this.ctlModulMenu.Attributes["onmouseout"] = "this.className-=' sfhover'";
            }

            if (mainNode.HasNodes)
            {
                this.ctlModulMenuLinkImage = new HtmlImage();

                this.ctlModulMenu.Controls.Add(this.ctlModulMenuLinkImage);

                this.ctlModulMenuLinkImage.Attributes["class"] = "ModuleOptionsMenuArrow";

                this.ctlModulMenuLinkImage.Attributes["src"] = this.ResolveUrl("skins/arrow.png");

                this.ctlModulMenuLinkImage.Attributes["alt"] = "Arrow";
            }

            this.ctlModulMenuLink = new LinkButton();

            if (!string.IsNullOrEmpty(mainNode.NavigateURL))
            {
                this.ctlModulMenuLink.Attributes["href"] = mainNode.NavigateURL;
                
                ////this.ctlModulMenuLink.CssClass = "ajax";
            }
            else
            {
                this.ctlModulMenuLink.Command += this.MenuItemClick;
                this.ctlModulMenuLink.CommandName = "MenuItem";

                this.ctlModulMenuLink.CommandArgument = mainNode.ID;
            }

            if (!string.IsNullOrEmpty(mainNode.JSFunction))
            {
                this.ctlModulMenuLink.Attributes.Add(
                    "onClick", string.Format("javascript:return {0};", mainNode.JSFunction));
            }

            this.ctlModulMenuLink.Attributes["id"] = string.Format("MenuItem{0}", mainNode.ID);
            this.ctlModulMenuLink.Attributes["title"] = !string.IsNullOrEmpty(mainNode.ToolTip)
                                                            ? mainNode.ToolTip
                                                            : mainNode.Text;
            
            if (!string.IsNullOrEmpty(mainNode.Image))
            {
                this.ctlModulMenuLinkImage = new HtmlImage();

                this.ctlModulMenuLinkImage.Attributes["src"] = this.SetIconPath(mainNode.Image, false);

                this.ctlModulMenuLinkImage.Attributes["alt"] = !string.IsNullOrEmpty(mainNode.ToolTip)
                                                            ? mainNode.ToolTip
                                                            : mainNode.Text;

                this.ctlModulMenuLink.Text = string.Format(
                    "<img src=\"{0}\" alt=\"{1}\" title\"{1}\" />&nbsp;{2}",
                    this.SetIconPath(mainNode.Image, false),
                    !string.IsNullOrEmpty(mainNode.ToolTip)
                                                            ? mainNode.ToolTip
                                                            : mainNode.Text,
                    !string.IsNullOrEmpty(mainNode.Text) ? mainNode.Text : "&nbsp;");
            }
            else
            {
                this.ctlModulMenuLink.Text = !string.IsNullOrEmpty(mainNode.Text) ? mainNode.Text : "&nbsp;";
            }

            if (mainNode.IsBreak)
            {
                this.ctlModulMenu.InnerHtml = "<hr />";
            }
            else
            {
                this.ctlModulMenu.Controls.Add(this.ctlModulMenuLink);
            }

            if (!mainNode.HasNodes)
            {
                return;
            }

            this.ctlModulMenu.Controls.Add(modulSubMenuUl);
            modulSubMenuUl.Attributes["class"] = mainNode.Text;

            foreach (DNNNode subNode in mainNode.DNNNodes)
            {
                this.RenderNode(subNode, modulSubMenuUl);
            }
        }

        /// <summary>
        /// The menu item click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void MenuItemClick(object sender, CommandEventArgs e)
        {
            try
            {
                this.ProcessAction(e.CommandArgument.ToString());
            }
            catch (Exception exception1)
            {
                Exceptions.ProcessModuleLoadException(this, exception1);
            }
        }

        /// <summary>
        /// Register Java Scripts for the Menu
        /// </summary>
        /// <param name="cssClassNameNormal">
        /// The CSS Class Name Normal.
        /// </param>
        /// <param name="cssClassNameContext">
        /// The CSS Class Name Context.
        /// </param>
        private void RegisterJavaScript(string cssClassNameNormal, string cssClassNameContext)
        {
            if (!this.bIncludeJs)
            {
                return;
            }

            var csType = typeof(Page);

            // JQuery & Plugins JS
            jQuery.RequestRegistration();

            if (HttpContext.Current.Items[string.Format("ModuleOptionsMenuScript{0}_registered", this.ClientID)] == null)
            {
                if (this.bContextMode)
                {
                    ScriptManager.RegisterClientScriptInclude(
                        this, csType, "jquerycontextMenu", this.ResolveUrl("jquery.contextMenu.js"));
                }
                else
                {
                    ScriptManager.RegisterClientScriptInclude(
                        this, csType, "jquerySuperFish", this.ResolveUrl("superfish.js"));

                    ScriptManager.RegisterClientScriptInclude(
                        this, csType, "jqueryhoverIntent", this.ResolveUrl("hoverIntent.js"));

                    ScriptManager.RegisterStartupScript(
                        this, 
                        csType, 
                        string.Format("ModuleOptionsMenu{0}", this.ClientID),
                        string.Format("jQuery(function(){{ jQuery('ul.{0}').superfish(); }});", cssClassNameNormal), 
                        true);
                    }

                HttpContext.Current.Items.Add(string.Format("ModuleOptionsMenuScript{0}_registered", this.ClientID), "true");
            }

            // Context Menu
            if (!this.bContextMode)
            {
                return;
            }

            string mainIconCss = cssClassNameNormal.Replace(
                            "ModuleOptionsMenu", "ModuleOptionsMenuMainIcon");

            if (!this.displayLink)
            {
                mainIconCss = string.Format("{0} MainLinkText", mainIconCss);
            }

            // Context Menu
            var sbContext = new StringBuilder();

            sbContext.Append("jQuery(document).ready( function() {");

            sbContext.AppendFormat("if (jQuery('#{0}').height() > 9 && jQuery('#{0}').width() > 9) {{", this.ClientID.Replace("dnnMODULEACTIONSMENU", "ModuleContent"));

            // Attach ContextMenu to Module Content
           /* sbContext.AppendFormat(
                "jQuery('#{0}').addcontextmenu('{1}');", this.ClientID.Replace("dnnMODULEACTIONSMENU", "ModuleContent"), this.ClientID);*/

             sbContext.AppendFormat(
                "jQuery('#{0}').vscontext({{menuBlock: '#{1}'}});", this.ClientID.Replace("dnnMODULEACTIONSMENU", "ModuleContent"), this.ClientID);

            sbContext.Append("} else {");

            if (!string.IsNullOrEmpty(this.contextAttachItemID))
            {
                 // Attach Context Menu to Module Title or Custom defined if Module content is to small to attach
                sbContext.AppendFormat("if (jQuery('#{0}') !== null & jQuery('#{0}').height() > 9 && jQuery('#{0}').width() > 9) {{", this.contextAttachItemID);

                /*sbContext.AppendFormat(
                "jQuery('#{0}').addcontextmenu('{1}');", this.contextAttachItemID, this.ClientID);*/

                sbContext.AppendFormat(
                "jQuery('#{0}').vscontext({{menuBlock: '#{1}'}});", this.contextAttachItemID, this.ClientID);

                sbContext.Append("} else {");

                // Fallback to Normal Menu
                sbContext.AppendFormat(
                    "jQuery('#{0}').removeClass('{1}');", this.ClientID, cssClassNameContext);

                sbContext.AppendFormat(
                    "jQuery('#{0}').wrap('<ul class=\"{1}\">').wrap('<li class=\"{2}\" id=\"MainNode{0}\">');",
                    this.ClientID,
                    cssClassNameNormal,
                    mainIconCss);

                sbContext.AppendFormat(
                    "jQuery('<img src=\"{1}\" alt=\"ModuleActionsMenu\" />').prependTo('#MainNode{0}');",
                    this.ClientID,
                    this.SetIconPath(this.MenuIcon, true));

                sbContext.Append("}");
            }
            else
            {
                // Fallback to Normal Menu
                sbContext.AppendFormat(
                    "jQuery('#{0}').removeClass('{1}');", this.ClientID, cssClassNameContext);

                sbContext.AppendFormat(
                    "jQuery('#{0}').wrap('<ul class=\"{1}\">').wrap('<li class=\"{2}\" id=\"MainNode{0}\">');",
                    this.ClientID,
                    cssClassNameNormal,
                    mainIconCss);

                var mainMenuBuilder = new StringBuilder();

                // Render Main Link Text
                if (!string.IsNullOrEmpty(this.sMenuLink) && this.displayLink)
                {
                    mainMenuBuilder.AppendFormat("<span>{0}</span>", this.sMenuLink);
                }

                // Render Main Icon
                if (!string.IsNullOrEmpty(this.sMenuIcon) && this.displayIcon)
                {
                    mainMenuBuilder.AppendFormat("<img src=\"{0}\" alt=\"ModuleActionsMenu\" title=\"Module Actions Menu\" />", this.SetIconPath(this.MenuIcon, true));
                }

                sbContext.AppendFormat("jQuery('{1}').prependTo('#MainNode{0}');", this.ClientID, mainMenuBuilder);
            }

            sbContext.Append("}");

            sbContext.Append("});");

            ScriptManager.RegisterStartupScript(
                this, csType, string.Format("ModuleOptionsMenuContext{0}", this.ClientID), sbContext.ToString(), true);
        }

        /// <summary>
        /// Set the Correct Image Icon Path
        /// </summary>
        /// <param name="image">
        /// Menu Image
        /// </param>
        /// <param name="isMainIcon">
        /// The is Main Icon.
        /// </param>
        /// <returns>
        /// The Final Resolved Image Path
        /// </returns>
        private string SetIconPath(string image, bool isMainIcon)
        {
            string fullImagePath;

            if (image.StartsWith("~/"))
            {
                fullImagePath = this.ResolveUrl(image);
            }
            else if (image.StartsWith("/"))
            {
                fullImagePath = image;
            }
            else
            {
                fullImagePath = isMainIcon
                                     ? this.ModulePath + image
                                     : this.ResolveUrl(string.Format("~/images/{0}", image));
            }

            return fullImagePath;
        }

        #endregion
    }
}