<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AllTotalRevenue.aspx.cs" Inherits="WebProject.AllTotalRevenue" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
      <meta charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <meta name="description" content=""/>
    <meta name="author" content=""/>

   
    <title>SB Admin 2 - Bootstrap Admin Theme</title>

    <!-- Bootstrap Core CSS -->
    <link href="./CSS/bootstrap.min.css" rel="stylesheet"/>

    <!-- MetisMenu CSS -->
    <link href="./CSS/metisMenu.min.css" rel="stylesheet"/>

    <!-- Custom CSS -->
    <link href="./CSS/sb-admin-2.css" rel="stylesheet"/>

    <!-- Morris Charts CSS -->
    <link href="./CSS/morris.css" rel="stylesheet"/>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
   <!-- Bootstrap Core JavaScript -->
    <script src="./CSS/bootstrap.min.js"></script>

    <!-- Metis Menu Plugin JavaScript -->
    <script src="./CSS/metisMenu.min.js"></script>

  <!-- Morris Charts JavaScript -->
    <script src="./CSS/raphael.min.js"></script>
    <script src="./CSS/morris.min.js"></script>
    <script src="./CSS/morris-data.js"></script>

    <!-- Custom Theme JavaScript -->
    <script src="./CSS/sb-admin-2.js"></script>
    <script>

       
</script>

</head>
<body>

  
    <form id="form1" runat="server">
          <asp:ScriptManager ID="_Script" runat="server" ></asp:ScriptManager>

    <div id="wrapper">

        <!-- Navigation -->
        <nav class="navbar navbar-default navbar-static-top" role="navigation" style="margin-bottom: 0">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a class="navbar-brand" href="index.html">SB Admin v2.0</a>
            </div>
            <!-- /.navbar-header -->

          

            <div class="navbar-default sidebar" role="navigation">
                <div class="sidebar-nav navbar-collapse">
                    <ul class="nav" id="side-menu">
                        <li >                       
       
                            <asp:Label ID="lblUserID" runat="server" Font-Size="Medium"></asp:Label>
                            <!-- /input-group -->
                        </li>
                        <li>
                            <a href="index.html"><i class="fa fa-dashboard fa-fw"></i> 게시판으로</a>
                        </li>
                        <li>
                            <a href="#"><i class="fa fa-bar-chart-o fa-fw"></i> 매출 현황<span class="fa arrow"></span></a>
                            <ul class="nav nav-second-level">
                                <li>
                                    <a href="flot.html">시간대 별 매출현황</a>
                                </li>
                                <li>
                                    <a href="morris.html">카테고리 별 매출현황</a>
                                </li>
                                <li>
                                    <a href="morris.html">품묵 별 매출현황</a>
                                </li>
                                <li>
                                    <a href="morris.html">결제수단 별 매출현황</a>
                                </li>
                                  <li>
                                    <a href="morris.html">객층분석</a>
                                </li>
                                 <li>
                                    <a href="morris.html">총매출 보기</a>
                                </li>
                            </ul>
                            <!-- /.nav-second-level -->
                        </li>
                        <li>
                            <a href="tables.html"><i class="fa fa-table fa-fw"></i> 재고 보기</a>
                        </li>                     
                    </ul>
                </div>
                <!-- /.sidebar-collapse -->
            </div>
            <!-- /.navbar-static-side -->
        </nav>

        <div id="page-wrapper">
            <div class="row">
                  <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">Tables</h1>
                </div>
                <!-- /.col-lg-12 -->
            </div>
            <!-- /.row -->
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            DataTables Advanced Tables
                        </div>
                        <!-- /.panel-heading -->
                        <div class="panel-body">
                            <table width="100%" class="table table-striped table-bordered table-hover" id="dataTables-example">
                              
                            </table>
                            <!-- /.table-responsive -->
                     
                        </div>
                        <!-- /.panel-body -->
                    </div>
                    <!-- /.panel -->
                </div>
                <!-- /.col-lg-12 -->
            </div>
            <!-- /.row -->
            <div class="row">
            </div>
            <!-- /.row -->
            <div class="row">
                <!-- /.col-lg-8 -->

                    <div class="panel panel-default">
                        <div>
                            &nbsp; &nbsp;
  
                        </div>
                        <!-- /.panel-heading -->
                        <div class="panel-body">
                            <!-- /.list-group -->
                          <div>
                            &nbsp; &nbsp;
                            <asp:TextBox runat="server" ID="DateSample" /><juice:Datepicker runat="server" TargetControlID="DateSample"/>
                            <asp:Label ID="Label1" runat="server" Text="~"></asp:Label>
                            <asp:TextBox ID="DateSample2" runat="server"></asp:TextBox>
                            <juice:Datepicker runat="server" TargetControlID="DateSample2"/>
                            <asp:Button ID="Button1" runat="server" Text="조회" Height="27px" Width="86px" OnClick="Button1_Click"/>
                        </div>
                        
                        </div>
                        <!-- /.panel-body -->
                    </div>
                    <!-- /.panel -->
                    <div class="panel panel-default">
                        <!-- /.panel-body -->
                    </div>
                    <!-- /.panel .chat-panel -->
      
            </div>
            <!-- /.row -->
        </div>
        <!-- /#page-wrapper -->

    </div>
    </form>
</body>
</html>
