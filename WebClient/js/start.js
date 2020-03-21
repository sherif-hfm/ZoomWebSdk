
const meetConfig = {
    apiKey: 'MbhIt-Z8T5iospJ_FaMNRw',
    leaveUrl: 'https://zoom.us',
    userName: 'Sherif Hamdy',
    meetingNumber: '7968284711',
    passWord: '',
    userEmail: 'sherif_hfm@yahoo.com',
    role: 1,
    ts: new Date().getTime()
};

var token;



function getSignature(meetConfig) {
    fetch('/apis/api/GetSignature', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(meetConfig)
    })
        .then(result => result.text())
        .then(response => {
            console.log('ok1 ' + response.replace(/"/g, ''));
            token = response.replace(/"/g, '');
            ZoomMtg.init({
                debug: true,
                leaveUrl: meetConfig.leaveUrl,
                isSupportAV: true,
                isShowJoiningErrorDialog: false,
                success: function () {
                    console.log('ok2 ' + response);
                    ZoomMtg.join({
                        signature: token,
                        apiKey: meetConfig.apiKey,
                        meetingNumber: meetConfig.meetingNumber,
                        userName: meetConfig.userName,
                        userEmail: meetConfig.userEmail,
                        // password optional; set by Host
                        passWord: meetConfig.passWord,
                        error: function (res) {
                            console.log('err1');
                            console.log(res);
                            setTimeout(() => { getSignature(meetConfig); }, 500);
                        },
                        success: function (e) {
                            console.log(e)
                        }
                    })
                },
                error: function (err) {
                    console.log('err2');
                    console.log(err)
                }
            })
        })
};

getSignature(meetConfig);

