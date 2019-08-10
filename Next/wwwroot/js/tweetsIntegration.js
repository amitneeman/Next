$(document).ready(() => {

    setTimeout(() => {
        var c = document.getElementById("tweeterlogo");
        var ctx = c.getContext("2d");
        var img = document.getElementById("twitter");
        ctx.drawImage(img, 0, 0);
    }, 1000);

    $.get("https://localhost:5001/Twitter/LinuxTweets").then((tweets) => {

        let tweetTemplate = (user, text) => (
            `
                <li class="tweetWrapper">

                    <div class="innerTweet">
                        <h4>
                            ${user}
                        </h4>
                        <p>
                        ${text}
                        </p>
                    
                    </div>


                </li>
            `
        );

        tweets = JSON.parse(tweets).statuses.map(tweet => tweetTemplate(tweet.user.name,tweet.text)).slice(0, 10);
        let list = `<ul class="tweetsList">${tweets.join(" ")}</ul>`

        document.getElementById("tweetArea").innerHTML = list;

    })

})