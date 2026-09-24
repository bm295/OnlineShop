export type OrderStatus = "pending" | "completed" | "cancelled";

export type Order = {
  id: number;
  customerName: string;
  totalAmount: number;
  createdAtUtc: string;
  status: OrderStatus;
};

export type CreateOrderInput = {
  customerName: string;
  totalAmount: number;
  status: OrderStatus;
};
