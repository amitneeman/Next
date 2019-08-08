$("#orderForm").change(async (data) => {
    let cpu = $('#CPU').val();
    let ram = $('#RAM').val();

    updatePrice(cpu, ram);
    updateMetrix(cpu, ram);


})

function updateMetrix(cpu, ram) {
    document.getElementById("cpucount").innerText = cpu;
    document.getElementById("ramcount").innerText = ram;
}

async function updatePrice(cpu, ram) {
    let dollarPrice = cpu + ram;
    let localPrice = "undetermined"

    if (cpu && ram) {

        let rate = 3.9
        try {
            rate = await dollarRate();
        } catch (err) {
            console.log(err)
        }
        localPrice = rate * dollarPrice + " ILS";
        dollarPrice += " $";
    }else {
        dollarPrice = "undetermined";
        localPrice = "undetermined";
    }

    document.getElementById("dollarprice").innerText = dollarPrice;
    document.getElementById("localprice").innerText = localPrice;
}