ZoomMtg.preLoadWasm();

ZoomMtg.prepareJssdk();

var userLangTemplate = $.i18n.getAll("en-US");
console.log(userLangTemplate);

var userLangDict = Object.assign({}, userLangTemplate, {
    'apac.wc_joining_meeting': 'الانضمام للجلسة'
    , 'apac.toolbar_join_audio': "الانضمام بالصوت"
    , 'apac.wc_audio': "الصوت"
    , 'apac.toolbar_mute': "الصوت"
    ,'apac.toolbar_muteall': "كتم الصوت"
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
    

});
$.i18n.load(userLangDict, "Arabic");

$.i18n.reload("Arabic");
