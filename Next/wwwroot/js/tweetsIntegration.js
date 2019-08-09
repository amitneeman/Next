$(document).ready(() => {

    $.get("https://localhost:5001/Twitter/LinuxTweets").then((tweets) => {

    tweets = JSON.parse(tweets).map(tweet => `<li>${tweet.text}</li>`).slice(0,10);
    let list = `<ul>${tweets.join(" ")}</ul>`

        document.getElementById("tweetArea").innerHTML = list;
    
})

})