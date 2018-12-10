export default {
  key: 1,
  email: "ana@google.com",
  firstName: "Ana",
  lastName: "Pociecha",
  orders: [
    {
      key: 1,
      orderEntries: [
        { key: 1, name: "Guatemala Antigua", quantity: 2, price: 2850},
        { key: 2, name: "Chocolate Turtle", quantity: 1, price: 1300}
      ],
      totalPrice: 4150,
      address: {
        country: "Polska",
        city: "Kraków",
        street: "ul. Budryka",
        ZIPCode: 30072,
        buildingNumber: 2,
        apartmentNumber: "1000A"
      },
      status: 1,
      openDate: "01-11-2018",
      closeDate: "12-11-2018"
    },
    {
      key: 2,
      orderEntries: [
        { key: 1, name: "Guatemala Antigua", quantity: 2, price: 2850}
      ],
      totalPrice: 2850,
      address: {
        country: "Polska",
        city: "Kraków",
        street: "ul. Budryka",
        ZIPCode: 30072,
        buildingNumber: 2,
        apartmentNumber: "1000A"
      },
      status: 0,
      openDate: "20-11-2018",
      closeDate: ""
    }
  ],
  receiveNewsletterEmail: 'apociecha@interia.pl'
};
