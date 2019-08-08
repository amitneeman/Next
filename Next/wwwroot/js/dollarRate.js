var dollarRate = () => {
    let ApiUrl = 'https://api.exchangeratesapi.io/latest?symbols=USD,ILS';

    return fetch(ApiUrl).then((resp) => resp.json()).then(data => {
        return data.rates.ILS
    }).catch((err) => {
        return 3.9
    })
}