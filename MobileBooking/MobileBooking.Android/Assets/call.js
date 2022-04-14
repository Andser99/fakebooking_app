let localVideo = document.getElementById("local-video")
let remoteVideo = document.getElementById("remote-video")

localVideo.style.opacity = 0
remoteVideo.style.opacity = 0

localVideo.onplaying = () => { localVideo.style.opacity = 1 }
remoteVideo.onplaying = () => { remoteVideo.style.opacity = 1 }

document.getElementById("init-button").onclick = () => {
    console.log(init(document.getElementById("my-id").value));
}

document.getElementById("call-button").onclick = () => {
    startCall(document.getElementById("other-id").value);
}


let peer;
function init(userId) {
    var config = { 'iceServers': [{ 'urls': ['stun:stun.l.google.com:19302'] }] };
    peer = new Peer(userId, config)

    listen();
    return peer;
}

let localStream
function listen() {
    peer.on('call', (call) => {

        navigator.getUserMedia({
            audio: true, 
            video: true
        }, (stream) => {
            localVideo.srcObject = stream
            localStream = stream

            call.answer(stream)
            call.on('stream', (remoteStream) => {
                remoteVideo.srcObject = remoteStream

                remoteVideo.className = "primary-video"
                localVideo.className = "secondary-video"
            })
        })
    })
}

function startCall(otherUserId) {
    navigator.getUserMedia({
        audio: true,
        video: true
    }, (stream) => {

        localVideo.srcObject = stream
        localStream = stream

        const call = peer.call(otherUserId, stream);
        console.log(peer);
        console.log(call);
        call.on('stream', (remoteStream) => {
            remoteVideo.srcObject = remoteStream

            remoteVideo.className = "primary-video"
            localVideo.className = "secondary-video"
        })

    })
}

function toggleVideo(b) {
    if (b == "true") {
        localStream.getVideoTracks()[0].enabled = true
    } else {
        localStream.getVideoTracks()[0].enabled = false
    }
} 

function toggleAudio(b) {
    if (b == "true") {
        localStream.getAudioTracks()[0].enabled = true
        
    } else {
        localStream.getAudioTracks()[0].enabled = false
    }
} 