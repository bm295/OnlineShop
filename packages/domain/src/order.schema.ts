import { z } from "zod";

export const createOrderSchema = z.object({
  customerName: z.string().trim().min(1),
  totalAmount: z.number().nonnegative(),
  status: z.enum(["pending", "completed", "cancelled"]),
});

export const orderSchema = createOrderSchema.extend({
  id: z.number().int().positive(),
  createdAtUtc: z.iso.datetime(),
});
