export const formatCurrency = function (amount) {
  return amount.toLocaleString("es-DO", {
    style: "currency",
    currency: "DOP",
  });
};
