   M S B A M L   `   `   `  ����\  XBrandonVanLoggerenberg_TimeKeeper, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullU/clr-namespace:BrandonVanLoggerenberg_TimeKeeper!BrandonVanLoggerenberg_TimeKeeper  5   	   3    -BrandonVanLoggerenberg_TimeKeeper.SetupWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Alocal/clr-namespace:BrandonVanLoggerenberg_TimeKeeper     5   	   -   5          � Title$  SetupWindow��5   	   $	��420��6   $��516.5��6*    � WindowStyle$
 None=�6O    � WindowStartupLocation$ CenterScreen=�6c   .��5	      � $
��Left=�6   $	��500��6&   .+�5
   
   �� -   5        $��SPSetupList��5
      $	��150��6(   $	��390��64   $
��Left=�6B   $	��Top=�6]   .+�5      � -   5        $��g_spWelcome��5      $��25��6   .+�5      �� $��	Welcome !��6   5      � 5      -   5        $��g_spConnect��5      $��25��6   .+�5      �� $��Connectivity��6   5      � 5      -   5        $��g_spUser��5      $��25��6   .+�5      �� $��User Create��6   5      � 5      -   5        $��g_spMisc��5      $��25��6   .+�5      �� $
��Misc��6   5      5      �� 5   
   -   5        $��SPSetupContent��5      $	��350��6+   $��Right=�68   $	��360��6T   $	��Top=�6a   .+�5      �� -   5         ��Name$ 
tabControl��5      $	��360��6-    �� SelectedIndex$ 0��6:   .@�5      �� $��Welcome��6   
 � Tag$ 0��6+   $y�	Collapsed=�63   .��5      � ���	#FFE5E5E5.+�5      �� .��5      �� Welcome to TimeKeeper!�� 5   "   �� 6.   :8Before you can start using TimeKeeper you need to set up�� 5!   "   a few things.5#      5$      �� 5%      $��0,100,20,20q�6    .��5&      �� 64Note : Later on I can add the Agree or Disagree with�� 5'   "   's and C's functionality here5(      5)      5*      5+      �� 5,      $��Connectivity��6   $ 1��60   $y�	Collapsed=�68   .��5-      � ���	#FFE5E5E5.+�5.      �� .��5/      �� <:Would you like to connect to a SQL Server instance or work�� 51   "   
offline?�� 52   "   �� 6.   ><(working offline means that other users won't be able to see�� 53   "   $"if you assign appointment to them)54      55      	� 56      -	   5         	�Name$ rBtn_Offline��56   &   $��Work Offline��6<   $
��Left=�6S   $��	5,110,0,0q�6n   $	��Top=�6�   $ 0��6�   	� 57      -
   5        $ rBtn_Online��57   &   $&�� Connect to a SQL Server Instance��6;   $
��Left=�6f   $��	5,130,0,0q�6�   $	��Top=�6�   $ 1��6�   �� 58      -   5        $��btn_ShowDatabaseCreate��58   -   $��20��6!   $��90��6K   $��Database Script��6V   $��245,130,0,0q�6p   $	��Top=�6�   $
��Left=�6�   6�   �� 59      $��Server Name :��6    $��	5,163,0,0q�68   $	��Top=�6K   $
��Left=�6c   �� 5:      -   5        $��txt_ServerName��5:   "   $��20��68   $��105,168,0,0q�6D   $	��230��6Y   $	��Top=�6f   $
��Left=�6~   {�.  6�   �� 5;      $��Database Name :��6    $��	5,190,0,0q�6:   $	��Top=�6M   $
��Left=�6e   �� 5<      -   5        $��txt_DatabaseName��5<   "   $��20��6:   $��105,195,0,0q�6F   $	��230��6[   $	��Top=�6h   $
��Left=�6�   {�.  6�   �� 5=      $��Authentication :��6    $��	5,220,0,0q�6;   $	��Top=�6O   $
��Left=�6g   �� 5>      -   5        $��cmb_ServerAuth��5>   #   $��20��69   $��105,225,0,0q�6E   $	��230��6Z   $	��Top=�6g   $
��Left=�6   {�.  .@�5?      �� .��6+   Windows Authentication6C   �� 5@      .��6+   SQL Server Authentication6F   5A      �� 5B      $��
Username :��6    $��	5,250,0,0q�65   $	��Top=�6I   $
��Left=�6a   �� 5C      -   5        $��txt_ServerUsername��5C   "   $��20��6<   $��105,255,0,0q�6H   $	��230��6]   $	��Top=�6j   $
��Left=�6�   {�.  6�   �� 5D      $��
Password :��6    $��	5,280,0,0q�65   $	��Top=�6I   $
��Left=�6a   �� 5E      -   5        $��txt_ServerPassword��5E   "   $��20��6<   $��105,285,0,0q�6H   $	��230��6]   $	��Top=�6i   $
��Left=�6�   {�.  6�   �� 5F      -   5        $��btn_TestConnection��5F   -   $��20��6!   $	��150��6G   $��Test Connection��6S   $��100,310,0,0q�6m   $	��Top=�6�   $
��Left=�6�   6�   �� 5G      $��Bottom=�6    .��6;   :8Settings can be changed inside of TimeKeeper at any time6u   5H      5I      �� 5J      $
��user��6   $ 2��6)   $y�	Collapsed=�61   .��5K      � ���	#FFE5E5E5.+�5L      �� .��5M      �� /-Great! Now you can create your Login Details.5O      5P      �� 5Q      -   5         ��Name$ label_username��5Q       $��
Username :��68   $
��Left=�6M   $��5,50,0,0q�6h   $	��Top=�6z   �� 5R      -   5         ��Name$ textBox_username��5R   "   $
��Left=�6<   $��23��6W   $��	70,55,0,0q�6c   	 �� TextWrapping$
	 Wrap=�6v   $�� ��6�   $	��Top=�6�   $	��200��6�   �� 5S      -   5        $ label_password��5S       $��
Password :��68   $
��Left=�6M   $��5,80,0,0q�6h   $	��Top=�6z   �� 5T      -   5        $ textBox_password��5T   "   $
��Left=�6<   $��23��6W   $��	70,85,0,0q�6c   $
	 Wrap=�6v   $�� ��6�   $	��Top=�6�   $	��200��6�   �� 5U      -   5        $��btn_CreateUser��5U   !   $��20��67   $��50��6C   $��Create��6N   $��215,120,0,0q�6_   $	��Top=�6u   $
��Left=�6�   6�   �� 5V      $��Bottom=�6    .��6;   :8Settings can be changed inside of TimeKeeper at any time6u   5W      5X      �� 5Y      $
��Misc��6   $ 3��6)   $y�	Collapsed=�61   .��5Z      � ���	#FFE5E5E5.+�5[      �� .��5\      �� 97A few last things before you can finally use TimeKeeper5^      5_      �� 5`      -   5        
 ��Name$
 gbTheme��5`   #   $��Theme��64   $
��Left=�6C   $��5,50,0,0q�6^   $	��Top=�6p   $	��325��6�   $	��150��6�   .��5a      � .+�5b   "   �� -   5         ��Name$ 	imageDark��5b   (   $	��100��6;   $	��150��6H   $
��Left=�6T   $��0,0,0,0q�6o   $	��Top=�6�   $��Images/DarkTheme.PNG��6�   �� 5c   "   -   5        $ 
imageLight��5c   (   $	��100��6<   $	��150��6I   $
��Left=�6U   $��	160,0,0,0q�6p   $	��Top=�6�   $��Images/LightTheme.PNG��6�   	� 5d   "   -   5        $ rBtn_Theme1��5d   .    	� 	GroupName$ ChosenTheme��6C   $
��Dark��6[   $
��Left=�6j   $��
50,100,0,0q�6�   $	��Top=�6�   	� 5e   "   -   5        $ rBtn_Theme2��5e   .   $ ChosenTheme��6C   $��Light��6[   $
��Left=�6k   $��180,100,0,0q�6�   $	��Top=�6�    d� 	IsChecked$
 TrueY�6�   5f      5g      �� 5h      -   5        $
 
gbCalendar��5h   #   $��First Day of Week��67   $
��Left=�6R   $��	5,200,0,0q�6m   $	��Top=�6�   $	��300��6�   $��70��6�   .��5i      � .+�5j   "   	� -   5        $ rBtn_Sun��5j   .   $ ChosenCalendar��6@   $��Sunday��6[   $
��Left=�6l   $��	50,20,0,0q�6�   $	��Top=�6�   	� 5k   "   -   5        $ rBtn_Mon��5k   .   $ ChosenCalendar��6@   $��Monday��6[   $
��Left=�6l   $��
180,20,0,0q�6�   $	��Top=�6�   $
 TrueY�6�   5l      5m      �� 5n      $��Bottom=�6    .��6;   :8Settings can be changed inside of TimeKeeper at any time6u   5o      5p      5r      5s      � 5t   
   -   5        $��SPNav��5t      $	��350��6   $��Right=�6(   $��Bottom=�6D   $��25��6_   .+�5u      �� -    5        $��	btn_Close��5u      $��50��6&   $��Center=�61   $
��Left=�6L   .��6�   Close6�   �� 5v      -!   5        $��btn_Previous��5v      $��50��6)   $��Center=�64   $��Right=�6O   $��	0,0,100,0q�6k   {�.  .��6�   
Previous6�   �� 5w      -"   5        $��btn_Next��5w      $��50��6%   $��Right=�60   $��0,2,25,3q�6L   .��6s   Next6y   5x      5z      5{      