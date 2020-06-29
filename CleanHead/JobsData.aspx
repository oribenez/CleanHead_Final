<%@ Page Title="" Language="C#" MasterPageFile="Dashboard.master" AutoEventWireup="true" CodeFile="JobsData.aspx.cs" Inherits="JobsData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
                /*------------Side-Bar------------*/
        .control_panel{
            color:#fff !important;
            background: #64B5F6 !important;
        }

        .popupBtnX{
            float:left;
        }
        .popupBack{
            display: block;
            z-index: 998;
            background: rgba(51, 51, 51, 0.6);
            position: fixed;
            width: 100%;
            height: 100%;
        }
        .popupContainer{
            background: #8FC4D7;
            border: solid #808080 4px;
            z-index: 999;
            position: fixed;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);      
            padding: 35px; 
        }

        /*table*/

        
        /*tbody tr:nth-child(2n+3){
            background: #eee;
        }
        table{
            border: 2px solid #ffffff;
            border-collapse:collapse
        }

        table th{
            border: 2px solid #ffffff;
            border-bottom-color: #d4dde4;
            background: rgba(212, 221, 228, .5);
            padding:2px 8px 4px;
        }
        td{
            padding: 1px 10px;
        }

        table tr:nth-child(2n+2){
            border: 2px solid #ffffff;
            font-size: 1em;
            box-shadow: inset 0 -1px 0 0 rgba(212, 221, 228, .5);
            background: rgba(212,221,228,0.15);
            padding: 6px 8px 8px;
            letter-spacing: -0.3px;
        }
        tbody tr:not(:first-child):hover{
            background: #b4ddec !important;
        }
        table td:nth-child(2){
            background: #8FC4D7;
            font-weight: bold;
            text-align: center;
        }*/
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PopUps" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageTitle" runat="server">תפקידים</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="PageLinks" runat="server"><a href="Default.aspx">ראשי</a> » <a href="ControlPanel.aspx">פאנל ניהול</a> » תפקידים
    <br /><br />
    <asp:Button ID="btnInsert" runat="server" Text="+" CssClass="btn-plus-header" CausesValidation="False" OnClick="btnInsert_Click" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="card-panel" style="margin: 40px 0 0 0;">
    <asp:GridView ID="gvJobs" runat="server" CellPadding="4" CssClass="table-striped" GridLines="None" OnLoad="gvJobs_Load" AutoGenerateColumns="False" DataKeyNames="job_id" AllowPaging="True" OnPageIndexChanging="gvJobs_PageIndexChanging" OnRowUpdating="gvJobs_RowUpdating" PageSize="20">          
            <Columns>
                <asp:TemplateField HeaderText="מזהה תפקיד">
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_edit_job_id" runat="server" Text='<%# Bind("job_id") %>' Width="35" ReadOnly="true"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lbl_show_job_id" runat="server" Text='<%# Bind("job_id") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="שם התפקיד">
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_edit_job_name" runat="server" Text='<%# Bind("job_name") %>' MaxLength="35" Width="115"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lbl_show_job_name" runat="server" Text='<%# Bind("job_name") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txt_insert_job_name" placeholder="שם התפקיד" runat="server" Width="115"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="עריכה" ShowHeader="False">
                    <EditItemTemplate>
                        <asp:ImageButton ID="btn_cancel_update_job" runat="server" CausesValidation="False" CommandName="CancelUpdate" ImageUrl="~/Images/ic_cancel_24px.png" Text="בטל" OnClick="btn_cancel_update_job_Click" />&nbsp;
                        <asp:ImageButton ID="btn_update_job" runat="server" CausesValidation="True" CommandName="Update" ImageUrl="~/Images/ic_done_24px.png" Text="עדכן" OnClick="btn_update_job_Click" />
                        
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:ImageButton ID="btn_edit_job" runat="server" CausesValidation="False" CommandName="Edit" ImageUrl="~/Images/ic_edit_24px.png" Text="ערוך" OnClick="btn_edit_job_Click" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:ImageButton ID="btn_insert_job" runat="server" CausesValidation="False" CommandName="Insert" ImageUrl="~/Images/ic_done_24px.png" Text="הוסף" OnClick="btn_insert_job_Click" />
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="מחיקה" ShowHeader="False">
                    <ItemTemplate>
                        <asp:ImageButton ID="btn_delete_job" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Images/ic_delete_24px.png" Text="מחק" OnClick="btn_delete_job_Click" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:ImageButton ID="btn_cancel_insert_job" runat="server" CausesValidation="False" CommandName="CancelInsert" ImageUrl="~/Images/ic_cancel_24px.png" Text="בטל" OnClick="btn_cancel_insert_job_Click" />
                    </FooterTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:Label ID="lblErrGV" runat="server" ForeColor="Red" Text=""></asp:Label>
    </div>
</asp:Content>

