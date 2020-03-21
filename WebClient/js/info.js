ZoomMtg.preLoadWasm();

ZoomMtg.prepareJssdk();

var userLangTemplate = $.i18n.getAll("en-US");
console.log(userLangTemplate);

var userLangDict = Object.assign({}, userLangTemplate, {
    'apac.wc_joining_meeting': 'الانضمام للجلسة'
    , 'apac.toolbar_join_audio': "الانضمام بالصوت"
    , 'apac.wc_audio': "الصوت"
    , 'apac.toolbar_mute': "كتم الصوت"
});
$.i18n.load(userLangDict, "Arabic");

$.i18n.reload("Arabic");
