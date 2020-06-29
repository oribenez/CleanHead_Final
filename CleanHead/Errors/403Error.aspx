<%@ Page Language="C#" AutoEventWireup="true" CodeFile="403Error.aspx.cs" Inherits="_403Error" %>

<!DOCTYPE html>

<html dir="rtl" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <style>
        * {
            margin: 0;
        }
        a {
            color: #F79521;
        }
        body {
            background: #262261;
        }
        #back {
            background: url('../Images/404Webpage-new_Background1.png');/*, url('../Images/404Webpage-new_Background.svg') no-repeat;*/
            width: 100%;
            height: 100%;
            background-size: cover;
            position: absolute;
        }
        #ground {
            background: url('../Images/404Webpage-ssnew2.png') bottom no-repeat;/*, url('../Images/404Webpage-ssnew.svg') bottom no-repeat;*/
            width: 100%;
            height: 100%;
            background-size: contain;
            position: absolute;
            bottom: 0;
        }
        #img404{
            background: url('../Images/404Webpage-new_Up1.png') no-repeat;/*, url('../Images/404Webpage-new_Up.svg') no-repeat;*/
            width: 29%;
            height: 29%;
            background-size: contain;
            position: absolute;
            float: left;
            margin: 0 60%;
            overflow: hidden;
        }
        #text{
            display: block;
            position: absolute;
            font-family: Tahoma;
            top: -200px;
            left: 0;
            right: 0;
            bottom: 0;
            margin: 0 7% 2% 0;
            width: 16%;
            height: 108px;
            font-size: 15px;
            background: rgba(32, 32, 32, 0.65);
            color: #CDCDCD;
            padding: 30px;
            box-shadow: 0px 0px 32px #000000;
            margin: auto auto;
            text-align: center;
        }
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="back">
            <div id="ground">
                <div id="text">
                    <h2>אין גישה לדף זה</h2>
                    <br />
                    <p>
                        אינך מורשה להיכנס לדף זה 
                         <a href="../Default.aspx">עבור לדף הבית מהרררר</a><br />
                        לפני שיהיה מאוחר מידי...
                    </p>
                </div>
                <div id="img404"></div>
            </div>
        </div>
    </form>
</body>
</html>
