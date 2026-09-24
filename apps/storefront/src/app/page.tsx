const summaryCards = [
  { label: "Products", value: "—" },
  { label: "Categories", value: "—" },
  { label: "Pending orders", value: "—" },
  { label: "Inventory value", value: "—" },
] as const;

export default function DashboardPage() {
  return (
    <main>
      <header className="pageHeader">
        <p className="eyebrow">OnlineShop</p>
        <h1>Dashboard</h1>
        <p className="subtitle">
          The TypeScript storefront is ready for product, category, and order
          features.
        </p>
      </header>

      <section aria-label="Shop summary" className="summaryGrid">
        {summaryCards.map((card) => (
          <article className="summaryCard" key={card.label}>
            <p>{card.label}</p>
            <strong>{card.value}</strong>
          </article>
        ))}
      </section>

      <section className="placeholderPanel">
        <h2>Recent orders</h2>
        <p>Order data will appear here after the API is connected.</p>
      </section>
    </main>
  );
}
