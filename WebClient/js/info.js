ZoomMtg.preLoadWasm();

ZoomMtg.prepareJssdk();

var userLangTemplate = $.i18n.getAll("en-US");
console.log(userLangTemplate);

var userLangDict = Object.assign({}, userLangTemplate, {
    'apac.wc_joining_meeting': 'الانضمام للجلسة'
    , 'apac.toolbar_join_audio': "الانضمام بالصوت"
    , 'apac.wc_audio': "الصوت"
    , 'apac.toolbar_mute': "الصوت"
    , 'apac.toolbar_muteall': "كتم الصوت"
    , 'apac.toolbar_unmuteall': "تفعيل الصوت"
    , 'apac.toolbar_unmute': "الصوت"
    , 'apac.meeting_is_host': "انت مدير الجلسة الان"
    , 'apac.wc_pc_audio': "صوت باستخدام كمبيوتر"
    , 'apac.wc_join_audio_by_pc': "الانضمام بالصوت"
    , 'apac.wc_video.start_video': "الفديو"
    , 'apac.wc_video.stop_video': "الفديو"
    , 'apac.wc_manage_participants': "المشاركين"
    , 'apac.dialog.end_meeting_confirm': "هل تريد بالتأكيد انهاء الجلسة ؟"
    , 'apac.dialog.end_meeting': "انهاء الجلسة"
    , 'apac.dialog.btn_endconf_meeting': "انهاء الجلسة"
    , 'apac.dialog.btn_leaveconf_meeting': "مغادرة الجلسة"
    , 'apac.wc_leave_meeting': "مغادرة الجلسة"
    , 'apac.dialog.meeting_over': " "
    , 'apac.wc_hostme': "(مدير الجلسة)"
    , 'apac.dialog.host_end_meeting': "الجلسة تم انهائها بواسطة مدير الجلسة."
    , 'apac.dialog.meeting_ended': "الجلسة تم انهائها"
    , 'apac.wc_other_is_host': "هو مدير الجلسة الان."
    , 'apac.dialog.leave_meeting_confirm': "هل تريد مغادرة الجلسة الان ؟"
    , 'apac.dialog.leave_meeting': "مغادرة الجلسة"
    , 'apac.wc_participants': "المشاركين"
    , 'apac.wc_me': "(أنا)"
    , 'apac.wc_host': "(مدير الجلسة)"
    , 'apac.wc_lower_hand': "خفض اليد"
    , 'apac.wc_raise_hand': "رفع اليد"
    , 'apac.wc_muteall_desc': "سوف يتم كتم الصوت عند جميع المشاركين"
    , 'apac.wc_continue': "الاستمرار"
    , 'apac.wc_qa.cancel': "الغاء الامر"
    , 'apac.wc_allow_unmute': "السماح للمشاركين بتشغيل الصوت"
    , 'apac.wc_more': "المزيد"
    , 'apac.wc_allow_rename': "السماح للمشاركين بتغير أسمائهم"
    , 'apac.wc_unlock_meeting': "تشغيل الجلسة"
    , 'apac.wc_lock_meeting': "تجميد الجلسة"
    , 'apac.dialog.rename': "تغيير الاسم"
    , 'apac.dialog.newname': "الاسم"
    , 'apac.dialog.btn_save': "حفظ"
    , 'apac.wc_qa.cancel': "الغاء الامر"
    , 'apac.dialog.btn_cancle': "الغاء الامر"
    , 'apac.wc_video.ask_start_video': "تشغيل الفديو"
    , 'apac.dialog.ok': "موافق"
    , 'apac.wc_make_host': "اسناد الإدارة"
    , 'apac.wc_remove': "حذف"
    , 'apac.wc_remove_user': "هل تريد حذف "
    , 'apac.wc_dlg_confirm': "تأكيد"
    , 'apac.wc_no': "لا"
    , 'apac.wc_yes': "نعم"
    , 'apac.dialog.you_are_removed': "تم إخراجك من الجلسة"
    , 'apac.dialog.meeting_removed_by_host': "تم إخراجك من الجلسة بواسطة المدير."
    , 'apac.dialog.btn_exit': "خروج"

});
$.i18n.load(userLangDict, "Arabic");

$.i18n.reload("Arabic");
