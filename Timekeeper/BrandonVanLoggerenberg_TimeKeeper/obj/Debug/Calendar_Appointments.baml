   M S B A M L   `   `   `  ����\  XBrandonVanLoggerenberg_TimeKeeper, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullU/clr-namespace:BrandonVanLoggerenberg_TimeKeeper!BrandonVanLoggerenberg_TimeKeeper  5   	   =    7BrandonVanLoggerenberg_TimeKeeper.Calendar_Appointments   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Alocal/clr-namespace:BrandonVanLoggerenberg_TimeKeeper     5   	   -   5          � Title$  Appointments��5   	   $	��421��6   $	��630��6+    � 
ResizeMode$ NoResize=�6N   .��5
      � -   5        $��MainGrid��5
   \   $	��630��6   $��0,0,-8,0q�6   $
��Left=�6)   $	��Top=�6D   $	��384��6l   .+�5   
   � -   5        $��AppointmentsHeader��5      $	��630��6)   $��25��65   $
��Left=�6B   $	��Top=�6]   $��0,0,0,0q�6u   .+�5      �� -   5        $��AppointmentsHeaderLabel��5      $
��test��63   $��292,0,188,1q�6B   $	��150��6W   5      � 5   
   $	��400��6   $��0,25,0,0q�6   $
��Left=�6-   $	��Top=�6H   .+�5      �� -   5        $��stackpanel_TimeLineBorder��5   {   $��
-20,10,0,0q�6   
���   �$��1q�6=    �� CornerRadius$ 1��6Q   $	��350��6b   $	��335��6n   ._�5      �� ��. .��5      �� -   5        $��Stackpanel_TimeLine��5   �   $
��Left=�6!   $	��Top=�6<   $	��335��6T    �� Orientation$ Vertical=�6a   $	��350��6x   5      5      5      5      � 5   
   -   5        $ ��AppointmentsHeader_Details��5      $	��210��61   $��25��6=   $
��Left=�6J   $	��Top=�6e   $��
398,25,0,0q�6}   .+�5      �� -   5        $%��AppointmentsHeader_DetailsLabel��5      $
��test��6;   $	��150��6J   5      � 5    
   -	   5        $��GridAppointmentDetails��5    p   $��
398,50,0,0q�6   $
��Left=�6#   $	��Top=�6>   $	��210��6W   $	��335��6c   .+�5!      �� -
   5         ��Name$ label_AppointmentHeader��5!   '   $��	44,-3,0,0q�6   $��Appointment Header :��6H   $
��Left=�6g   $	��Top=�6�   �� 5"      -   5         ��Name$ textBox_AppointmentHeader��5"      $
��Left=�69   $��23��6T   $��	11,22,0,0q�6`    �� TextWrapping$
 Wrap=�6s   $	��Top=�6�   $	��191��6�   6�   �� 5$      -   5        $" label_AppointmentDescription��5$   '   $��	35,41,0,0q�6   $��Appointment Description :��6M   $
��Left=�6q   $	��Top=�6�   �� 5%      -   5        $$ textBox_AppointmentDescription��5%       �� AcceptsReturn .  �� 
AcceptsTab . $
 Wrap=�6e   	 �� 	IsEnabled	 . $
��Left=�6�   $��97��6�   $��	10,65,0,0q�6�   $	��Top=�6�   $	��191��6�   6�   �� 5'      -   5        $ label_Attendees��5'   (   $��
70,158,0,0q�6   $��Attendees :��6A   $
��Left=�6W   $	��Top=�6r   � 5)      $	��200��6   $��	5,180,0,0q�6   $
��Left=�62   $	��Top=�6M   .+�5*      �� -   5        $ ��stackpanel_AttendeesBorder��5*   R   
���   �$��1q�6-   $ 1��6A   ._�5+      �� ��. .��5,      �� -   5        $��Stackpanel_Attendees��5,   �   $
��Left=�6%   $	��Top=�6@   $��90��6X   $ Vertical=�6d   $	��190��6{   5.      5/      50      51      �� 53      -   5        $��button_Save��53      $��152,301,0,0q�6(   $
��Left=�6>   $	��Top=�6Y   $��30��6q   $��50��6}   .��6�   Save6�   �� 55      -   5        $��button_Cancel��55      $��
10,300,0,0q�6*   $
��Left=�6@   $	��Top=�6[   $��30��6s   $��50��6   .��6�   Cancel6�   57      58      59      