<%@ Page Title="" Language="C#" MasterPageFile="Dashboard.master" AutoEventWireup="true" CodeFile="About.aspx.cs" Inherits="About" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style> 
        #about{
            background:#2196F3 !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PopUps" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageTitle" Runat="Server">
    אודות
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageLinks" Runat="Server">
    <a href="Default.aspx">ראשי</a> » אודות
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="card-panel">
        
        <img src="Images/120781-graduation-cap-clip-art.jpg" width="70" />
        <h4>לתלמידים</h4>
        <div class="flow-text" style="font-size: 1.08rem;">
            בבתי ספר שמפעילים את מערכת "ראש-נקי", מקבל כל תלמיד סיסמה, המאפשרת לו להכנס לכרטיס התלמיד שלו במערכת.
            כך, יכולים התלמידים לצפות בנתונים שונים: דוחות ציונים, רישומי התנהגות, שיעורי בית, לוח חופשות עדכני ממשרד החינוך, הודעות.
            מטרת החיבור של התלמידים למערכת "ראש-נקי" הינה לאפשר שותפות בניהול תהליך הלמידה. המידע הקיים אצל המורים הופך להיות שקוף גם בפני התלמידים, באופן יומיומי.
        </div>

        <h4>למורים</h4>
        <div class="flow-text" style="font-size: 1.08rem;">
            כל אנשי הצוות החינוכי מזינים למערכת "ראש-נקי", וכולם משתמשים בדוחות.
            הפאזל המתקבל משפע הנתונים המוזנים ממקורות שונים מאפשר תמונה מקיפה על תלמיד וקבוצת לימוד.
            מורים ואנשי צוות מזינים בזמן אמת את המידע הרלונטי באמצעות מחשב.
            הנתונים מוזרמים ישירות למאגר המידע הבית ספרי וזמינים לשליפה בצורה של טבלאות ודוחות.
        </div>
        
        <h4>צור קשר</h4>
        <div class="flow-text" style="font-size: 1.08rem;">
            האתר נבנה באמצעות אורי בן עזרא. לעוד מידע ניתן לפנות למספר הפלאפון: 054-832-1468
        </div>
    </div>
</asp:Content>

