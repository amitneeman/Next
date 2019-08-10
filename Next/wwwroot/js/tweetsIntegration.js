$(document).ready(() => {

    setTimeout(() => {
        var c = document.getElementById("tweeterlogo");
        var ctx = c.getContext("2d");
        var img = document.getElementById("twitter");
        ctx.drawImage(img, 0, 0);
    }, 1000);

    $.get("https://localhost:5001/Twitter/LinuxTweets").then((tweets) => {

        let tweetTemplate = (user,userpic, text) => (
            `
                <li class="tweetWrapper">

                    <div class="innerTweet">
                        <div class="tweetuserarea">
                            <img class="profileimg" src="${userpic}"/>
                            <h4 class="tweetusername">${user}</h4>
                        </div>
                        <p>
                        ${text}
                        </p>                 
                    </div>


                </li>
            `
        );

        tweets = JSON.parse(tweets).statuses.map(tweet => tweetTemplate(tweet.user.name, tweet.user.profile_image_url,tweet.text)).slice(0, 10);
        let list = `<ul class="tweetsList">${tweets.join(" ")}</ul>`

        document.getElementById("tweetArea").innerHTML = list;

    })

})