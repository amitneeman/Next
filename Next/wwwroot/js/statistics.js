﻿function initOsStats() {
    $.get("https://localhost:5001/AdminStatistics/OSCountStats").then((res) => {
        let data = {}
        for(let os of res){
            data[os.name] = os.count
        }
        var width = 450
        height = 450
        margin = 40
        var radius = Math.min(width, height) / 2 - margin


        var svg = d3.select("#osChart")
            .append("svg")
            .attr("width", width)
            .attr("height", height)
            .append("g")
            .attr("transform", "translate(" + width / 2 + "," + height / 2 + ")");
            

        var color = d3.scaleOrdinal()
            .domain(data)
            .range(d3.schemeSet2);

        var pie = d3.pie()
            .value(function (d) { return d.value; })
        var data_ready = pie(d3.entries(data))

        var arcGenerator = d3.arc()
            .innerRadius(0)
            .outerRadius(radius)

        svg
            .selectAll('mySlices')
            .data(data_ready)
            .enter()
            .append('path')
            .attr('d', arcGenerator)
            .attr('fill', function (d) { return (color(d.data.key)) })
            .attr("stroke", "black")
            .style("stroke-width", "2px")
            .style("opacity", 0.7)

        // Now add the annotation. Use the centroid method to get the best coordinates
        svg
            .selectAll('mySlices')
            .data(data_ready)
            .enter()
            .append('text')
            .text(function (d) { return d.data.key + ": " + d.data.value })
            .attr("transform", function (d) { return "translate(" + arcGenerator.centroid(d) + ")"; })
            .style("text-anchor", "middle")
            .style("font-size", 17)
    })
}

function initDataCenterChart() {
    $.get("https://localhost:5001/AdminStatistics/ServerPerDC").then((response) => {


    // set the dimensions and margins of the graph
    var margin = { top: 30, right: 30, bottom: 70, left: 60 },
        width = 460 - margin.left - margin.right,
        height = 400 - margin.top - margin.bottom;

    // append the svg object to the body of the page
    var svg = d3.select("#DCstats")
        .append("svg")
        .attr("width", width + margin.left + margin.right)
        .attr("height", height + margin.top + margin.bottom)
        .append("g")
        .attr("transform",
            "translate(" + margin.left + "," + margin.top + ")");

        var data = response.map(item => ({DC: item.name , count: item.count}))

        // X axis
        var x = d3.scaleBand()
            .range([0, width])
            .domain(data.map(function (d) { return d.DC; }))
            .padding(0.2);
        svg.append("g")
            .attr("transform", "translate(0," + height + ")")
            .call(d3.axisBottom(x))
            .selectAll("text")
            .attr("transform", "translate(-10,0)rotate(-45)")
            .style("text-anchor", "end");

        // Add Y axis
        var y = d3.scaleLinear()
            .domain([0, 20])
            .range([height, 0]);
        svg.append("g")
            .call(d3.axisLeft(y));

        // Bars
        svg.selectAll("mybar")
            .data(data)
            .enter()
            .append("rect")
            .attr("x", function (d) { return x(d.DC); })
            .attr("y", function (d) { return y(d.count); })
            .attr("width", x.bandwidth())
            .attr("height", function (d) { return height - y(d.count); })
            .attr("fill", "#4b91ce")
})
}


function initUsersChart() {
    $.get("https://localhost:5001/AdminStatistics/ServersPerUser").then((response) => {


    // set the dimensions and margins of the graph
    var margin = { top: 30, right: 30, bottom: 70, left: 60 },
        width = 460 - margin.left - margin.right,
        height = 400 - margin.top - margin.bottom;

    // append the svg object to the body of the page
    var svg = d3.select("#DCstats")
        .append("svg")
        .attr("width", width + margin.left + margin.right)
        .attr("height", height + margin.top + margin.bottom)
        .append("g")
        .attr("transform",
            "translate(" + margin.left + "," + margin.top + ")");

        var data = response.map(item => ({user: item.user.split('@')[0] , count: item.count}))

        // X axis
        var x = d3.scaleBand()
            .range([0, width])
            .domain(data.map(function (d) { return d.user; }))
            .padding(0.2);
        svg.append("g")
            .attr("transform", "translate(0," + height + ")")
            .call(d3.axisBottom(x))
            .selectAll("text")
            .attr("transform", "translate(-10,0)rotate(-45)")
            .style("text-anchor", "end");

        // Add Y axis
        var y = d3.scaleLinear()
            .domain([0, 20])
            .range([height, 0]);
        svg.append("g")
            .call(d3.axisLeft(y));

        // Bars
        svg.selectAll("mybar")
            .data(data)
            .enter()
            .append("rect")
            .attr("x", function (d) { return x(d.user); })
            .attr("y", function (d) { return y(d.count); })
            .attr("width", x.bandwidth())
            .attr("height", function (d) { return height - y(d.count); })
            .attr("fill", "#d04832")
})
}

initOsStats();
initDataCenterChart();
initUsersChart();