   M S B A M L   `   `   `  ����\  XBrandonVanLoggerenberg_TimeKeeper, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullU/clr-namespace:BrandonVanLoggerenberg_TimeKeeper!BrandonVanLoggerenberg_TimeKeeper  5   	   >    8BrandonVanLoggerenberg_TimeKeeper.Appointment_Acceptance   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Alocal/clr-namespace:BrandonVanLoggerenberg_TimeKeeper     5   	   -   5          � Title$  New Appointments��5   	   $	��355��6"   $	��650��6/    � 
ResizeMode$ NoResize=�6R    � WindowStartupLocation$ CenterScreen=�6    � WindowStyle$
 None=�6�   .��5	      � -   5        $��MainGrid��5	      .+�5
   
   � -   5        $��AppointmentsHeader��5
      $	��650��6*   $��25��66   $
��Left=�6C   $	��Top=�6^   $��0,0,0,0q�6v   .+�5      �� -   5        $��AppointmentsHeaderLabel��5   5   $��Appointment Acceptance��6   $	��150��6T   $��25��6`   $��166,-4,174,4q�6l   5      � 5   
   -   5        $��AppointmentsContent��5      $	��430��6+   $	��330��67   $
��Left=�6E   $	��Top=�6`   $��0,25,0,0q�6x   .+�5      � -   5        $��AppointmentsContainer��5      .+�5      �� -   5        $��stackpanel_TimeLineBorder��5   ~   $��	0,-30,0,0q�6   
���   �$��1q�6@    �� CornerRadius$ 1��6T   $	��420��6e   $	��300��6q   ._�5      �� ��. .��5      �� -   5        $��Stackpanel_TimeLine��5   �   $
��Left=�6%   $	��Top=�6@   $	��300��6X    �� Orientation$ Vertical=�6e   $	��420��6|   5      5      5      5      �� 5      -	   5        $��button_Reject��5      $��Reject��6+   $��75��6<   $��25��6G   $��-150,305,0,0q�6S   6i   �� 5      -
   5        $��button_Reject_All��5      $��
Reject All��6.   $��75��6C   $��25��6N   $��-340,305,0,0q�6Z   6p   �� 5      -   5        $��button_Accept��5      $��Accept��6+   $��75��6<   $��25��6G   $��150,305,0,0q�6S   6h   �� 5      -   5        $��button_Accept_All��5      $��
Accept All��6/   $��75��6D   $��25��6O   $��340,305,0,0q�6[   6p   5      � 5!   
   -   5        $ ��AppointmentsHeader_Details��5!      $	��210��61   $��25��6=   $
��left=�6J   $	��Top=�6e   $��
435,25,0,0q�6}   .+�5"      �� -   5        $%��AppointmentsHeader_DetailsLabel��5"      $
��test��6;   $	��150��6J   5#      � 5%   
   -   5        $��GridAppointmentDetails��5%   p   $��
435,55,0,0q�6   $
��Left=�6#   $	��Top=�6>   $	��210��6W   $	��300��6c   .+�5&      �� -   5         ��Name$ label_AppointmentHeader��5&   '   $��	44,-5,0,0q�6   $��Appointment Header :��6H   $
��Left=�6g   $	��Top=�6�   �� 5'      -   5         ��Name$ textBox_AppointmentHeader��5'      $
��Left=�69   $��23��6T   $��	11,17,0,0q�6`    �� TextWrapping$
 Wrap=�6s   $	��Top=�6�   $	��191��6�   	 �� 
IsReadOnly	 . 6�   �� 5)      -   5        $" label_AppointmentDescription��5)   '   $��	35,40,0,0q�6   $��Appointment Description :��6M   $
��Left=�6q   $	��Top=�6�   �� 5*      -   5        $$ textBox_AppointmentDescription��5*      
 �� AcceptsReturn
 .  �� 
AcceptsTab . $
 Wrap=�6e    �� 	IsEnabled . $
��Left=�6�   $��97��6�   $��	10,60,0,0q�6�   $	��Top=�6�   $	��191��6�   	 . 6�   �� 5,      -   5        $ label_Attendees��5,   (   $��
70,155,0,0q�6   $��Attendees :��6A   $
��Left=�6W   $	��Top=�6r   � 5.      $	��200��6   $��	5,175,0,0q�6   $
��Left=�62   $	��Top=�6M   .+�5/      �� -   5        $ ��stackpanel_AttendeesBorder��5/   R   
���   �$��1q�6-   $ 1��6A   ._�50      �� ��. .��51      �� -   5        $��Stackpanel_Attendees��51   �   $
��Left=�6%   $	��Top=�6@   $��90��6X   $ Vertical=�6d   $	��190��6{   53      54      55      56      �� 57      -   5        $��button_Cancel��57      $��Cancel��6*   $��55��6;   $��25��6F   $��-150,275,0,0q�6R   6h   �� 58      -   5        $��button_Finish��58      $��Finish��6*   $��55��6;   $��25��6F   $��150,275,0,0q�6R   6g   59      5:      5;      