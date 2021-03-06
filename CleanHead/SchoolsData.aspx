﻿<%@ Page Title="" Language="C#" MasterPageFile="Dashboard.master" AutoEventWireup="true" CodeFile="SchoolsData.aspx.cs" Inherits="SchoolsData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        /*------------Side-Bar------------*/
        #control_panel {
            background: url(Images/control_panel_selected.png) no-repeat;
        }
        .control_panel:hover > #control_panel {
            background: url(Images/control_panel_selected.png) no-repeat;
        }
        .control_panel{
            color:#fff !important;
            background: #64B5F6 !important;
        }


        .popupBtnX {
            float: left;
        }

        .popupBack {
            display: block;
            z-index: 998;
            background: rgba(51, 51, 51, 0.6);
            position: fixed;
            width: 100%;
            height: 100%;
        }

        .popupContainer {
            background: #8FC4D7;
            border: solid #808080 4px;
            z-index: 999;
            position: fixed;
            top: 50%;
            left: 50%;
            -moz-transform: translate(-50%, -50%);
            -ms-transform: translate(-50%, -50%);
            -o-transform: translate(-50%, -50%);
            -webkit-transform: translate(-50%, -50%);
            transform: translate(-50%, -50%);
            padding: 35px;
        }

        /*table*/

        /*tbody tr:nth-child(2n+3) {
            background: #eee;
        }

        table {
            border: 2px solid #ffffff;
            border-collapse: collapse;
        }

            table th {
                border: 2px solid #ffffff;
                border-bottom-color: #d4dde4;
                background: rgba(212, 221, 228, .5);
                padding: 2px 8px 4px;
            }

        td {
            padding: 1px 10px;
        }

        table tr:nth-child(2n+2) {
            border: 2px solid #ffffff;
            font-size: 1em;
            box-shadow: inset 0 -1px 0 0 rgba(212, 221, 228, .5);
            background: rgba(212,221,228,0.15);
            padding: 6px 8px 8px;
            letter-spacing: -0.3px;
        }

        tbody tr:not(:first-child):hover {
            background: #b4ddec !important;
        }

        table td:nth-child(3) {
            background: #8FC4D7;
            font-weight: bold;
            text-align: center;
        }*/
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PopUps" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageTitle" runat="server">בתי ספר</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="PageLinks" runat="server"><a href="Default.aspx">ראשי</a> » <a href="ControlPanel.aspx">פאנל ניהול</a> » בתי ספר
    <br />
    <br />
    <asp:Button ID="btnInsert" runat="server" Text="+" CssClass="btn-plus-header" CausesValidation="False" OnClick="btnInsert_Click" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="card-panel" style="margin: 40px 0 0 0;">
        <%-- SearchBox --%>
        <div class="float-right">
            <div class="form-group search-box float-right">
                <asp:TextBox CssClass="material" ID="txtSearch" runat="server"></asp:TextBox>
                <span class="form-highlight"></span>
                <span class="form-bar"></span>
                <asp:Label ID="Label1" AssociatedControlID="txtSearch" CssClass="form-label" runat="server" Text="חיפוש"></asp:Label>
            </div>
            <asp:Button ID="btnSearch" CssClass="float-right" runat="server" Text="חפש" CausesValidation="false" style="margin: 45px 10px 0 0;" OnClick="btnSearch_Click" />
        </div>
        <asp:GridView ID="gvSchools" runat="server" CellPadding="4" GridLines="None" CssClass="table-striped" OnLoad="gvSchools_Load" AutoGenerateColumns="False" DataKeyNames="sc_id" AllowPaging="True" OnPageIndexChanging="gvSchools_PageIndexChanging" OnRowDataBound="gvSchools_RowDataBound" OnRowUpdating="gvSchools_RowUpdating" PageSize="20">
            <Columns>
                <asp:TemplateField HeaderText="מזהה בית ספר">
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_edit_sc_id" runat="server" Text='<%# Bind("sc_id") %>' Width="35" ReadOnly="true"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lbl_show_sc_id" runat="server" Text='<%# Bind("sc_id") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="סמל בית ספר">
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_edit_sc_symbol" runat="server" Text='<%# Bind("sc_symbol") %>' Width="115" MaxLength="6"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lbl_show_sc_symbol" runat="server" Text='<%# Bind("sc_symbol") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txt_insert_sc_symbol" placeholder="סמל בית הספר" runat="server" Width="115" MaxLength="6"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="שם בית ספר">
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_edit_sc_name" runat="server" Text='<%# Bind("sc_name") %>' MaxLength="35" Width="115"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lbl_show_sc_name" runat="server" Text='<%# Bind("sc_name") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txt_insert_sc_name" placeholder="שם בית הספר" runat="server" MaxLength="35" Width="115"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="עיר">
                    <EditItemTemplate>
                        <asp:DropDownList ID="txt_edit_ddlCities" runat="server"></asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lbl_show_cty_name" runat="server" Text='<%# Bind("cty_name") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="txt_insert_ddlCities" runat="server"></asp:DropDownList>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="כתובת">
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_edit_sc_address" runat="server" Text='<%# Bind("sc_address") %>' MaxLength="35" Width="115"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lbl_show_sc_address" runat="server" Text='<%# Bind("sc_address") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txt_insert_sc_address" placeholder="כתובת" runat="server" MaxLength="35" Width="115"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="טלפון">
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_edit_sc_telephone" runat="server" Text='<%# Bind("sc_telephone") %>' MaxLength="9" Width="115"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lbl_show_sc_telephone" runat="server" Text='<%# Bind("sc_telephone") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txt_insert_sc_telephone" placeholder="טלפון" runat="server" MaxLength="9" Width="115"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="עריכה" ShowHeader="False">
                    <EditItemTemplate>
                        <asp:ImageButton ID="btn_cancel_update_sc" runat="server" CausesValidation="False" CommandName="CancelUpdate" ImageUrl="~/Images/ic_cancel_24px.png" Text="בטל" OnClick="btn_cancel_update_sc_Click" />&nbsp;
                        <asp:ImageButton ID="btn_update_sc" runat="server" CausesValidation="True" CommandName="Update" ImageUrl="~/Images/ic_done_24px.png" Text="עדכן" OnClick="btn_update_sc_Click" />
                        
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:ImageButton ID="btn_edit_sc" runat="server" CausesValidation="False" CommandName="Edit" ImageUrl="~/Images/ic_edit_24px.png" Text="ערוך" OnClick="btn_edit_sc_Click" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:ImageButton ID="btn_insert_sc" runat="server" CausesValidation="False" CommandName="Insert" ImageUrl="~/Images/ic_done_24px.png" Text="הוסף" OnClick="btn_insert_sc_Click" />
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="מחיקה" ShowHeader="False">
                    <ItemTemplate>
                        <asp:ImageButton ID="btn_delete_sc" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Images/ic_delete_24px.png" Text="מחק" OnClick="btn_delete_sc_Click" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:ImageButton ID="btn_cancel_insert_sc" runat="server" CausesValidation="False" CommandName="CancelInsert" ImageUrl="~/Images/ic_cancel_24px.png" Text="בטל" OnClick="btn_cancel_insert_sc_Click" />
                    </FooterTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:Label ID="lblErrGV" runat="server" ForeColor="Red" Text=""></asp:Label>
    </div>
</asp:Content>

