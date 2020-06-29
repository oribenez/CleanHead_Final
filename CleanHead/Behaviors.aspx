<%@ Page Title="" Language="C#" MasterPageFile="Dashboard.master" AutoEventWireup="true" CodeFile="Behaviors.aspx.cs" Inherits="Behaviors" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        #behavior{
            fill: #fff !important;
        }
        .behavior:hover > #behavior{
            fill: #fff !important;
        }
        .behavior{
            color:#fff !important;
            background: #64B5F6 !important;
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


        .lblSumPoints{
            font-size: 20px;
        }

        /*-----filter----*/
        /*.filtersWrap{
            background: #b4ddec;
            border: 1px solid #8FC4D7;
            padding:10px;
            margin: 0;
        }*/
        /*-----filter----*/
        
    </style>
    <script>
        function Relationship() {
            alert("לא ניתן ל מחוק שדה זה משום שהוא קשור בקשרי גומלין עם שדות אחרים.\nכדי למחוק שדה זה, יש למחוק את השדות הקשורים לשדה זה");
        }
        function ConfirmDelete() {
            if ("<%=Session["gender"].ToString() %>" == "זכר") {
                return confirm("האם אתה בטוח שאתה רוצה למחוק את ההתנהגות של התלמיד?");
            }
            else {
                return confirm("האם את בטוחה שאת רוצה למחוק את ההתנהגות של התלמיד?");
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageTitle" runat="server">דוח התנהגות</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="PageLinks" runat="server"><a href="Default.aspx">ראשי</a> » דוח התנהגות
    <%if (Convert.ToInt32(Session["lvl_id"]) > 0) { %>
    <br /><br />
        <asp:Button ID="btnInsert" runat="server" Text="+" CssClass="btn-plus-header" CausesValidation="False" OnClick="btnInsert_Click" />
    <%} %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="card-panel" style="margin: 40px 0 0 0;">
        <%if (Convert.ToInt32(Session["lvl_id"]) > 0) { 
	         %>
        <h4>סינון</h4>
            <%} %>
        <div class="filtersWrap">
        
            <%if (Convert.ToInt32(Session["lvl_id"]) >= 4) { 
	         %>
            <asp:DropDownList ID="ddlSchools" runat="server" OnLoad="ddlSchools_Load" 
                OnSelectedIndexChanged="ddlSchools_SelectedIndexChanged"
                AutoPostBack="True"></asp:DropDownList>
            &nbsp;←&nbsp;
            <%} %>

            <%if (Convert.ToInt32(Session["lvl_id"]) >= 1) { // אם מורה ומעלה
	         %>
            <asp:DropDownList ID="ddlLayers" runat="server" OnLoad="ddlLayers_Load" 
                OnSelectedIndexChanged="ddlLayers_SelectedIndexChanged" 
                AutoPostBack="True"></asp:DropDownList>
            &nbsp;<b>←</b>&nbsp;

                <%if (Convert.ToInt32(Session["lvl_id"]) == 1) { // אם מורה
	             %>
                    <asp:DropDownList ID="ddlProfessions" runat="server" OnLoad="ddlProfessions_Load" 
                        OnSelectedIndexChanged="ddlProfessions_SelectedIndexChanged"
                        AutoPostBack="True"></asp:DropDownList>
                    &nbsp;<b>←</b>&nbsp;
                    <asp:DropDownList ID="ddlTeachers" AppendDataBoundItems="false" runat="server" OnLoad="ddlTeachers_Load" 
                        OnSelectedIndexChanged="ddlTeachers_SelectedIndexChanged"
                        AutoPostBack="True"></asp:DropDownList>
                    &nbsp;<b>←</b>&nbsp;
        
                    <asp:DropDownList ID="ddlLessons" runat="server" AppendDataBoundItems="false" OnLoad="ddlLessons_Load" 
                        onselectedindexchanged="ddlLessons_SelectedIndexChanged" 
                        AutoPostBack="True" ClientIDMode="AutoID"></asp:DropDownList>
                    &nbsp;<b>←</b>&nbsp;
                <%} %>

                <%if (Convert.ToInt32(Session["lvl_id"]) >= 3) { // אם עורך בית ספר ומעלה
	             %>
                    <asp:DropDownList ID="ddlClasses" runat="server" OnLoad="ddlClasses_Load"
                        OnSelectedIndexChanged="ddlClasses_SelectedIndexChanged"
                        AutoPostBack="True"></asp:DropDownList>
                    &nbsp;<b>←</b>&nbsp;
                <%} %>
            בחר תלמיד: 
            <asp:ListBox ID="lbStudents" AutoPostBack="True" runat="server" OnSelectedIndexChanged="lbStudents_SelectedIndexChanged"></asp:ListBox>
            <%} %>
        </div>
        <br />
        <asp:Panel ID="pnlBhv" runat="server">
            <asp:Label ID="lblSumPoints" CssClass="lblSumPoints" runat="server" Text=''></asp:Label><br />
            <asp:GridView ID="gvBhv" runat="server" OnRowCreated="gvBhv_RowCreated" OnLoad="gvBhv_Load" CssClass="table-striped" OnPageIndexChanging="gvBhv_PageIndexChanging" AllowPaging="True" PageSize="16" AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField HeaderText="תאריך">
                        <ItemTemplate>
                            <asp:Label ID="lblBhvDate" runat="server" Text='<%# Bind("stu_bhv_date", "{0:dd/MM/yy}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="שעה">
                        <ItemTemplate>
                            <asp:Label ID="lblHrName" runat="server" Text='<%# Bind("hr_name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="סוג">
                        <ItemTemplate>
                            <asp:Label ID="lblBhvName" runat="server" Text='<%# Bind("bhv_name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
            
                    <asp:TemplateField HeaderText="שווי(בנקודות)">
                        <ItemTemplate>
                            <asp:Label ID="lblBhvValue" runat="server" Text='<%# Bind("bhv_value") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
            
                    <asp:TemplateField HeaderText="מקצוע">
                        <ItemTemplate>
                            <asp:Label ID="lblProName" runat="server" Text='<%# Bind("pro_name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
            
                    <asp:TemplateField HeaderText="שיעור">
                        <ItemTemplate>
                            <asp:Label ID="lblLesName" runat="server" Text='<%# Bind("les_name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
            
                    <asp:TemplateField HeaderText='ע"י מורה'>
                        <ItemTemplate>
                            <asp:Label ID="lblByTchFirstName" runat="server" Text='<%# Bind("usr_first_name") %>'></asp:Label>
                             <asp:Label ID="lblByTchLastName" runat="server" Text='<%# Bind("usr_last_name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="מחיקה" ShowHeader="False">
                        <ItemTemplate>
                            <% if (Convert.ToInt32(Session["lvl_id"]) >= 3 || Convert.ToInt32(Session["lvl_id"]) == 1) { %>
                                <asp:ImageButton ID="btn_delete_bhv" runat="server" CausesValidation="False" ImageUrl="~/Images/ic_delete_24px.png" Text="מחק" OnClientClick="return ConfirmDelete();" OnClick="btn_delete_bhv_Click" />
                            <% } %>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </asp:Panel>
        <asp:Label ID="lblErrGV" runat="server" ForeColor="Red" Text=""></asp:Label>
    </div>
</asp:Content>

